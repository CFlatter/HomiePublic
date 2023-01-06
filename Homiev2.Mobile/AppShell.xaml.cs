using Homiev2.Mobile.Services;
using Homiev2.Mobile.Views;

namespace Homiev2.Mobile
{
    public partial class AppShell : Shell
    {


        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CreateHouseholdMemberPageView), typeof(CreateHouseholdMemberPageView));
            Routing.RegisterRoute(nameof(EditChorePageView), typeof(EditChorePageView));
        }

    }
}