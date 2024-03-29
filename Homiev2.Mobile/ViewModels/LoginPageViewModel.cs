﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Homiev2.Mobile.Services;
using Homiev2.Mobile.Views;

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
            //InitializeAsync();
        }

        // private async void InitializeAsync()
        //{
        //    if (await _authService.CheckForValidCachedJwtToken())
        //    {
        //        App.Current.MainPage = new AppShell();
        //        await Shell.Current.GoToAsync("//MainPageView");
        //    }
        //}

        [RelayCommand]
        private async Task Login()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                await _authService.GetTokenAsync(_username, _password);
                App.Current.MainPage = new AppShell();
                await Shell.Current.GoToAsync("//MainPageView");

            }
            catch (UnauthorizedAccessException)
            {
                await Application.Current.MainPage.DisplayAlert("Login Failed", "Please try again", "Close");
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

        [RelayCommand]
        private async Task Register()
        {
            if (IsBusy)
                return;
            RegisterPageViewModel registerPageViewModel = new RegisterPageViewModel(_authService);
            App.Current.MainPage = new RegisterPageView(registerPageViewModel);

        }
    }
}
