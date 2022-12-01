using Homiev2.Mobile.ViewModels;
using Homiev2.Shared.Dto;

namespace Homiev2.Mobile.Views
{
    public partial class MainPageView : ContentPage
    {
        private readonly MainPageViewModel _viewModel;

        public MainPageView(MainPageViewModel viewModel)
        {
            InitializeComponent();            
            _viewModel = viewModel;
            this.BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Task.Run(async() => await _viewModel.GetChoresAsync());
        }

    }
}