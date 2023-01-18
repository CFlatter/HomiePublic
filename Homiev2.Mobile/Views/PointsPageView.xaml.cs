using Homiev2.Mobile.Enum;
using Homiev2.Mobile.ViewModels;

namespace Homiev2.Mobile.Views;

public partial class PointsPageView : ContentPage
{
	private readonly PointsPageViewModel _viewModel;

	public PointsPageView(PointsPageViewModel viewModel)
	{
		_viewModel = viewModel;
        BindingContext = _viewModel;
        InitializeComponent();
    }

	private async void timespanPicker_SelectedIndexChanged(object sender, EventArgs e)
	{
		_viewModel.SelectedTimespan = (PointsLeaderboardTimespans)timespanPicker.SelectedIndex;
		await _viewModel.GetPointsAsync();
	}
}