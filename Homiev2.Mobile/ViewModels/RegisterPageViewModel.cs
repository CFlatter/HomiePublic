using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Homiev2.Mobile.Services;
using Homiev2.Mobile.Views;

namespace Homiev2.Mobile.ViewModels
{
    public partial class RegisterPageViewModel : BaseViewModel
    {
        private readonly AuthService _authService;
        [ObservableProperty]
        private string _emailAddress;

        [ObservableProperty]
        private string _password;


        [ObservableProperty]
        private string _friendlyName;

        public RegisterPageViewModel(AuthService authService)
        {
            this.Title = "Login";
            _authService = authService;

        }


        [RelayCommand]
        private async Task Register()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                await _authService.RegisterAsync(_emailAddress, _password, _friendlyName);
                LoginPageViewModel loginPageViewModel = new LoginPageViewModel(_authService);
                App.Current.MainPage = new LoginPageView(loginPageViewModel);

            }
            catch (UnauthorizedAccessException)
            {
                await Application.Current.MainPage.DisplayAlert("Registration Failed", "Please try again", "Close");
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error Occured", e.Message, "Close");
            }
            finally
            {
                IsBusy = false;
            }


        }

    }
}
