using Homiev2.Mobile.ViewModels;

namespace Homiev2.Mobile.Views;

public partial class RegisterPageView : ContentPage
{
	public RegisterPageView(RegisterPageViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}

}