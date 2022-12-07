using Homiev2.Mobile.Services;
using Homiev2.Mobile.ViewModels;
using Homiev2.Mobile.Views;

namespace Homiev2.Mobile
{
    public partial class App : Application
    {
        public App(LoadingPageViewModel loadingPageViewModel)
        {
            
            MainPage = new LoadingPageView(loadingPageViewModel);
            InitializeComponent();

        }



    }
}