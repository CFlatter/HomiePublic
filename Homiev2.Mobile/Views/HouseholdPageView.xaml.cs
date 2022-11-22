using Homiev2.Mobile.ViewModels;

namespace Homiev2.Mobile.Views;

public partial class HouseholdPageView : ContentPage
{
	public HouseholdPageView(HouseholdPageViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;
	}

}