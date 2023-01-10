using CommunityToolkit.Mvvm.Input;
using Homiev2.Mobile.Enum;
using Homiev2.Mobile.Services;
using Homiev2.Mobile.Views;
using Homiev2.Shared.Dto;
using Homiev2.Shared.Models;
using MonkeyCache.FileStore;
using System.Collections.ObjectModel;

namespace Homiev2.Mobile.ViewModels
{
    public partial class HouseholdPageViewModel : BaseViewModel
    {

        public ObservableCollection<HouseholdMember> HouseholdMembers { get; private set; }
        public string ShareCode { get; private set; }  

        private readonly ApiService _apiService;

        public HouseholdPageViewModel(ApiService apiService)
        {
            Title = "Your Household";
            _apiService = apiService;
            HouseholdMembers = new();
            InitializeAsync();
            Barrel.ApplicationId= "Homie";
        }

        public async void InitializeAsync()
        {
            await GetHouseholdMembersAsync();
            await GetShareCodeAsync();
        }

        async Task GetHouseholdMembersAsync()
        {
            if (IsBusy)
                return;


            IsBusy = true;
            try
            {

                if (!Barrel.Current.IsExpired(key: "household_members"))
                {
                    var householdMembers = Barrel.Current.Get<List<HouseholdMember>>("household_members");
                    if (householdMembers != null)
                    {
                        if (HouseholdMembers.Count != 0)
                        {
                            HouseholdMembers.Clear();
                        }

                        foreach (var member in householdMembers)
                        {
                            HouseholdMembers.Add(member);
                        }
                    }
                }
                else
                {
                    var householdMembers = await _apiService.ApiRequestAsync<List<HouseholdMember>>(ApiRequestType.GET, "HouseholdMember/HouseholdMembers");
                    if (householdMembers != null)
                    {
                        if (HouseholdMembers.Count != 0)
                        {
                            HouseholdMembers.Clear();
                        }

                        foreach (var member in householdMembers)
                        {
                            HouseholdMembers.Add(member);
                        }

                        Barrel.Current.Add(key: "household_members", data: ShareCode, expireIn: TimeSpan.FromHours(1));
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
                
        }

        async Task GetShareCodeAsync()
        {

            try
            {
                if (!Barrel.Current.IsExpired(key: "share_code"))
                {
                    ShareCode = Barrel.Current.Get<string>("share_code");
                }
                else
                {
                    var household = await _apiService.ApiRequestAsync<HouseholdDTO>(ApiRequestType.GET, "Household/Household");
                    if (household != null)
                    {
                        ShareCode = household.ShareCode;
                        Barrel.Current.Add(key: "share_code", data: ShareCode, expireIn: TimeSpan.FromDays(30));
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


        }

        [RelayCommand]
        async Task CreateNewHouseholdMemberAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(CreateHouseholdMemberPageView)}");
        }

        [RelayCommand]
        async Task JoinHouseholdAsync(string shareCode)
        {
            JoinHouseholdDto joinHouseholdDto = new()
            {
                ShareCode = shareCode,
            };
            await _apiService.ApiRequestAsync<JoinHouseholdDto>(ApiRequestType.POST, "HouseholdMember/JoinHousehold", joinHouseholdDto);
            InitializeAsync();
        }
    }
}
