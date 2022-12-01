using CommunityToolkit.Mvvm.Input;
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
        private readonly HouseholdPageViewModel _householdPageViewModel;
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

        public MainPageViewModel(ApiService apiService, HouseholdPageViewModel householdPageViewModel)
        {
            Title = "Home";
            _apiService = apiService;
            _householdPageViewModel = householdPageViewModel;
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

        [RelayCommand]
        private async Task CompleteChoreAsync(BaseChore chore)
        {
            string[] householdMemberOptions = new string[_householdPageViewModel.HouseholdMembers.Count];

            for (int i = 0; i < _householdPageViewModel.HouseholdMembers.Count; i++)
            {
                householdMemberOptions[i] = _householdPageViewModel.HouseholdMembers[i].MemberName;
            }

            string chosenHouseholdMember = await Shell.Current.DisplayActionSheet("Who completed the chore?", "Cancel", null, householdMemberOptions);

            Guid householdMemberId = _householdPageViewModel.HouseholdMembers.Where(x => x.MemberName == chosenHouseholdMember).Select(x => x.HouseholdMemberId).First();

            CompletedChoreDto completedChore = new();
            completedChore.ChoreId = chore.ChoreId;
            completedChore.HouseholdMemberId = householdMemberId;

            await _apiService.ApiRequestAsync<CompletedChoreResponseDto>(ApiRequestType.POST, "Chore/CompleteChore", completedChore);
            
        }

        [RelayCommand]
        private async Task SkipChoreAsync(BaseChore chore)
        {
            string[] householdMemberOptions = new string[_householdPageViewModel.HouseholdMembers.Count];

            for (int i = 0; i < _householdPageViewModel.HouseholdMembers.Count; i++)
            {
                householdMemberOptions[i] = _householdPageViewModel.HouseholdMembers[i].MemberName;
            }

            string chosenHouseholdMember = await Shell.Current.DisplayActionSheet("Who is skipping the chore?", "Cancel", null, householdMemberOptions);

            Guid householdMemberId = _householdPageViewModel.HouseholdMembers.Where(x => x.MemberName == chosenHouseholdMember).Select(x => x.HouseholdMemberId).First();

            CompletedChoreDto completedChore = new();
            completedChore.ChoreId = chore.ChoreId;
            completedChore.HouseholdMemberId = householdMemberId;
            completedChore.Skipped = true;

            await _apiService.ApiRequestAsync<CompletedChoreResponseDto>(ApiRequestType.POST, "Chore/CompleteChore", completedChore);

        }


    }
}
