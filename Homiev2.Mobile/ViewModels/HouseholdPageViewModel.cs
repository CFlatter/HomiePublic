using CommunityToolkit.Mvvm.Input;
using Homiev2.Mobile.Enum;
using Homiev2.Mobile.Services;
using Homiev2.Mobile.Views;
using Homiev2.Shared.Models;
using System.Collections.ObjectModel;

namespace Homiev2.Mobile.ViewModels
{
    public partial class HouseholdPageViewModel : BaseViewModel
    {

        public ObservableCollection<HouseholdMember> HouseholdMembers { get; private set; }

        private readonly ApiService _apiService;

        public HouseholdPageViewModel(ApiService apiService)
        {
            Title = "Your Household";
            _apiService = apiService;
            HouseholdMembers = new();
            InitializeAsync();
        }

        public async void InitializeAsync()
        {
            await GetHouseholdMembersAsync();
        }

        async Task GetHouseholdMembersAsync()
        {
            if (IsBusy)
                return;


                IsBusy = true;
            try
            {
                var members = await _apiService.ApiRequestAsync<List<HouseholdMember>>(ApiRequestType.GET, "HouseholdMember/HouseholdMembers");
                if (HouseholdMembers.Count != 0)
                {
                    HouseholdMembers.Clear();
                }

                foreach (var member in members)
                {
                    HouseholdMembers.Add(member);
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
        async Task CreateNewHouseholdMemberAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(CreateHouseholdMemberPageView)}");
        }
    }
}
