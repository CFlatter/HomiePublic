using Homiev2.Mobile.ViewModels;

namespace Homiev2.Mobile.Views;

public partial class LoadingPageView : ContentPage
{
	private readonly LoadingPageViewModel _viewModel;

	public LoadingPageView(LoadingPageViewModel viewModel)
	{	
		_viewModel = viewModel;
        InitializeComponent();
    }
}