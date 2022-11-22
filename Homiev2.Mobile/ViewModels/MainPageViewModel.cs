using Homiev2.Mobile.Enum;
using Homiev2.Mobile.Services;
using Homiev2.Mobile.Views;
using Homiev2.Shared.Dto;
using Homiev2.Shared.Models;
using System.Collections.ObjectModel;

namespace Homiev2.Mobile.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;

        private ObservableCollection<BaseChore> _chores;
        public ObservableCollection<BaseChore> Chores
        {
            get
            {
                if (_chores.Any())
                    return (ObservableCollection<BaseChore>)_chores.OrderByDescending(x => x.NextDueDate);
                else
                    return _chores;
            }
        }

        public MainPageViewModel(ApiService apiService)
        {
            Title = "Home";
            _apiService = apiService;
            _chores = new();
        }
     
        public async Task GetChoresAsync()
        {
            if (IsBusy)
                return;


            IsBusy = true;
            try
            {
                var chores = await _apiService.ApiRequestAsync<List<BaseChore>>(ApiRequestType.GET, "Chore/Chores");
                if (Chores.Count != 0)
                {
                    _chores.Clear();
                }

                foreach (var chore in chores)
                {
                    _chores.Add(chore);
                }

            }
            catch (UnauthorizedAccessException)
            {
                await Shell.Current.DisplayAlert("Please login", "login has expired", "Dismiss");
                await Shell.Current.GoToAsync($"{nameof(LoginPageView)}");
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Error", e.Message, "Dismiss");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task CompleteChoreAsync(CompletedChoreDto completedChore)
        {
            await _apiService.ApiRequestAsync<string> (ApiRequestType.POST,"Chore/CompleteChore",completedChore);
        }
    }
}
