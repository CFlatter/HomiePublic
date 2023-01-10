using Homiev2.Mobile.Services;
using Homiev2.Mobile.ViewModels;
using Homiev2.Mobile.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Skip)]
namespace Homiev2.Mobile
{
    public partial class App : Application
    {
        public App(LoadingPageViewModel loadingPageViewModel)
        {
            Application.Current.UserAppTheme = AppTheme.Light;
            MainPage = new LoadingPageView(loadingPageViewModel);
            InitializeComponent();

        }



    }
}