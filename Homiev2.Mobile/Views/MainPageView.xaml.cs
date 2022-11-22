using Homiev2.Mobile.ViewModels;

namespace Homiev2.Mobile.Views
{
    public partial class MainPageView : ContentPage
    {
        private readonly MainPageViewModel _viewModel;

        public MainPageView(MainPageViewModel viewModel)
        {
            InitializeComponent();
            this.BindingContext = viewModel;
            _viewModel = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Task.Run(async() => await _viewModel.GetChoresAsync());
        }

        async void CompleteChoreClicked(object sender, EventArgs args)
        {
            await _viewModel.
        }

    }
}