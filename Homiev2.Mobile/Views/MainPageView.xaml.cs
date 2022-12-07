using Homiev2.Mobile.ViewModels;
using Homiev2.Shared.Dto;

namespace Homiev2.Mobile.Views
{
    public partial class MainPageView : ContentPage
    {
        private readonly MainPageViewModel _viewModel;

        public MainPageView(MainPageViewModel viewModel)
        {                      
            _viewModel = viewModel;
            this.BindingContext = viewModel;
            Task.Run(async () => await _viewModel.GetChoresAsync(true));
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.GetChoresAsync();
        }

    }
}