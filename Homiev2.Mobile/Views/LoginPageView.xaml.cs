using Homiev2.Mobile.ViewModels;

namespace Homiev2.Mobile.Views;

public partial class LoginPageView : ContentPage
{
	public LoginPageView(LoginPageViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}

}