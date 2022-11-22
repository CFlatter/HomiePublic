using Homiev2.Mobile.Services;
using Homiev2.Mobile.ViewModels;
using Homiev2.Mobile.Views;

namespace Homiev2.Mobile
{
    public partial class App : Application
    {
        private readonly AuthService _authService;
        private bool _validToken;

        public App(AuthService authService, LoginPageViewModel loginPageViewModel)
        {
            InitializeComponent();
            _validToken = false;
            _authService = authService;
            CheckValidBearerTokenAsync();

            if (_validToken)
            {
                MainPage = new AppShell();
            }
            else
            {
                MainPage =  new LoginPageView(loginPageViewModel);
            }
            
            
        }
        void CheckValidBearerTokenAsync()
        {
            _validToken = _authService.IsBearerTokenValid();
        }
    }
}