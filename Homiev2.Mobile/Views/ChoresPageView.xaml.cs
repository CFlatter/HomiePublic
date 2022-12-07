using Homiev2.Mobile.ViewModels;

namespace Homiev2.Mobile.Views;

public partial class ChoresPageView : ContentPage
{
	private readonly ChoresPageViewModel _viewModel;

	public ChoresPageView(ChoresPageViewModel choresTabbedPageViewModel)
	{		
		_viewModel = choresTabbedPageViewModel;
		this.BindingContext = _viewModel;
        InitializeComponent();
    }
}