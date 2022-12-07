using Homiev2.Mobile.Services;
using Homiev2.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homiev2.Mobile.ViewModels
{
    public class LoadingPageViewModel : BaseViewModel
    {
        private readonly Task initTask;
        private readonly AuthService _authService;
        private readonly LoginPageViewModel _loginPageViewModel;

        public LoadingPageViewModel(AuthService authService, LoginPageViewModel loginPageViewModel)
        {            
            _authService = authService;
            _loginPageViewModel = loginPageViewModel;
            this.initTask = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            if (await _authService.CheckForValidCachedJwtToken())
            {
                App.Current.MainPage = new AppShell();
            }
            else
            {
                App.Current.MainPage = new LoginPageView(_loginPageViewModel);
            }
        }
    }
}
