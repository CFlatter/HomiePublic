
using Homiev2.Mobile.Interfaces;
using Homiev2.Shared.Models;
using System.Collections.ObjectModel;

namespace Homiev2.Mobile.ViewModels
{
    public partial class HouseholdViewModel : BaseViewModel
    {
        public ObservableCollection<HouseholdMember> HouseholdMembers { get; } = new();
        public Command GetHouseholdMembersCommand { get; }

        IHouseholdService householdService;

        public HouseholdViewModel(IHouseholdService householdService)
        {
            Title = "Your Household";
            this.householdService = householdService;
            GetHouseholdMembersCommand = new Command(async () => await GetHouseholdMembersAsync());
        }

        async Task GetHouseholdMembersAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                var members = await householdService.GetAllHouseholdMembersAsync();

                if (HouseholdMembers.Count != 0)
                {
                    HouseholdMembers.Clear();
                }

                foreach (var member in members)
                {
                    HouseholdMembers.Add(member);
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
