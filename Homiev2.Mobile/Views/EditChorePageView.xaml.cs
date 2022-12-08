using Homiev2.Mobile.Services;
using Homiev2.Mobile.ViewModels;
using Homiev2.Shared.Dto;

namespace Homiev2.Mobile.Views;

public partial class EditChorePageView : ContentPage
{
	private readonly EditChorePageViewModel _viewModel;
	private readonly ApiService _apiService;

	public EditChorePageView(ApiService apiService , BaseChoreDto choreDto = null)
	{
		_viewModel = new EditChorePageViewModel(apiService, choreDto);
		BindingContext = _viewModel;
		_apiService = apiService;
        InitializeComponent();
    }
}