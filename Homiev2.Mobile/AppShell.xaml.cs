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
            Routing.RegisterRoute(nameof(AddChorePageView), typeof(AddChorePageView));
            Routing.RegisterRoute(nameof(RegisterPageView), typeof(RegisterPageView));
            Routing.RegisterRoute(nameof(LoginPageView), typeof(LoginPageView));
        }

    }
}