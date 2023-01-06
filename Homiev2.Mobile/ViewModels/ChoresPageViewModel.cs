using CommunityToolkit.Mvvm.Input;
using Homiev2.Mobile.Enum;
using Homiev2.Mobile.Services;
using Homiev2.Mobile.Views;
using Homiev2.Shared.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.ObjectModel;

namespace Homiev2.Mobile.ViewModels
{
    public partial class ChoresPageViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;
        private readonly Task _initTask;
        //private readonly EditChorePageViewModel _editChorePageViewModel;

        public ObservableCollection<BaseChoreDto> Chores { get; private set; }


        public ChoresPageViewModel(ApiService apiService/*, EditChorePageViewModel editChorePageViewModel*/)
        {
            _apiService = apiService;
            //_editChorePageViewModel = editChorePageViewModel;
            Title = "Chores";
            Chores = new();
            _initTask = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            await GetChoresAsync();
        }

        private async Task GetChoresAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                if (Chores.Count != 0)
                {
                    Chores.Clear();
                }

                var chores = await _apiService.ApiRequestAsync<List<BaseChoreDto>>(ApiRequestType.GET, "Chore/Chores");

                foreach (var chore in chores)
                {
                    Chores.Add(chore);
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

        [RelayCommand]
        public async Task AddChoreAsync(BaseChoreDto chore)
        {
            await Shell.Current.GoToAsync(nameof(EditChorePageView), true, new Dictionary<string, object> { { "Chore", chore } });
        }

        [RelayCommand]
        public async Task EditChoreAsync(BaseChoreDto chore)
        {
            await Shell.Current.GoToAsync(nameof(EditChorePageView), true, new Dictionary<string,object> { { "Chore" , chore} });
        }

        [RelayCommand]
        public async Task DeleteChoreAsync(BaseChoreDto chore)
        {
            await _apiService.ApiRequestAsync<string>(ApiRequestType.DELETE, $"Chore/DeleteChore/{chore.ChoreId}");
        }

        [RelayCommand]
        public async Task Refresh()
        {
            IsRefreshing = true;
            await InitializeAsync();
            IsRefreshing = false;            
        }
    }
}
