using Homiev2.Mobile.Views;

namespace Homiev2.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(LoginPageView), typeof(LoginPageView));
        }
    }
}