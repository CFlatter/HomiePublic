using CommunityToolkit.Mvvm.Input;
using Homiev2.Mobile.Enum;
using Homiev2.Mobile.Services;
using Homiev2.Mobile.Views;
using Homiev2.Shared.Dto;
using MonkeyCache.FileStore;
using System.Collections.ObjectModel;

namespace Homiev2.Mobile.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;
        private readonly HouseholdPageViewModel _householdPageViewModel;
        private ObservableCollection<BaseChoreDto> _chores;
        public ObservableCollection<BaseChoreDto> Chores
        {
            get
            {
                if (_chores.Any())
                {
                    return _chores;
                }
                else
                {
                    return _chores;
                }

            }
        }

        public MainPageViewModel(ApiService apiService, HouseholdPageViewModel householdPageViewModel)
        {
            Title = "Home";
            _apiService = apiService;
            _householdPageViewModel = householdPageViewModel;
            Barrel.ApplicationId = "Homie";
            _chores = new();
        }


        public async Task GetChoresAsync(bool forceSync = false)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                if (_chores.Count != 0)
                {
                    _chores.Clear();
                }

                if (!Barrel.Current.IsExpired(key: "chores") && forceSync == false)
                {
                    //await Task.Yield();//Because we are getting this from the cache and its a syncronous call
                    var cachedChores = Barrel.Current.Get<List<BaseChoreDto>>(key: "chores");
                    cachedChores.Sort((l, r) => l.NextDueDate.CompareTo(r.NextDueDate));

                    foreach (var chore in cachedChores)
                    {
                        _chores.Add(chore);
                    }

                }
                else
                {
                    var chores = await _apiService.ApiRequestAsync<List<BaseChoreDto>>(ApiRequestType.GET, "Chore/Chores");

                    chores.Sort((l, r) => l.NextDueDate.CompareTo(r.NextDueDate));

                    Barrel.Current.Add(key: "chores", data: chores, expireIn: TimeSpan.FromMinutes(30));

                    foreach (var chore in chores)
                    {
                        _chores.Add(chore);
                    }
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
        private async Task CompleteChoreAsync(BaseChoreDto chore)
        {
            string[] householdMemberOptions = new string[_householdPageViewModel.HouseholdMembers.Count];

            for (int i = 0; i < _householdPageViewModel.HouseholdMembers.Count; i++)
            {
                householdMemberOptions[i] = _householdPageViewModel.HouseholdMembers[i].MemberName;
            }

            string chosenHouseholdMember = await Shell.Current.DisplayActionSheet("Who completed the chore?", "Cancel", null, householdMemberOptions);

            Guid householdMemberId = _householdPageViewModel.HouseholdMembers.Where(x => x.MemberName == chosenHouseholdMember).Select(x => x.HouseholdMemberId).First();

            CompletedChoreDto completedChore = new()
            {
                ChoreId = chore.ChoreId,
                HouseholdMemberId = householdMemberId,
                Skipped = false,
                CompletedDateTime = DateTime.Now //TODO expand so user can select Date/Time completed
            };

            try
            {
                await _apiService.ApiRequestAsync<CompletedChoreResponseDto>(ApiRequestType.POST, "Chore/CompleteChore", completedChore);
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Error", e.Message, "Dismiss");
            }
            finally
            {
                await GetChoresAsync(true);
            }


        }

        [RelayCommand]
        private async Task SkipChoreAsync(BaseChoreDto chore)
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
            try
            {
                await _apiService.ApiRequestAsync<CompletedChoreResponseDto>(ApiRequestType.POST, "Chore/CompleteChore", completedChore);
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Error", e.Message, "Dismiss");
            }
        }


    }
}
