using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Homiev2.Mobile.Services;

namespace Homiev2.Mobile.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        private readonly AuthService _authService;
        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _password;

        public LoginPageViewModel(AuthService authService)
        {
            this.Title = "Login";
            _authService = authService;
        }

        [RelayCommand]
        async Task Login()
        {
            IsBusy = true;
            await _authService.GetTokenAsync(_username, _password);
            IsBusy = false;
        }
    }
}
