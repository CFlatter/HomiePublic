using Homiev2.Mobile.ViewModels;

namespace Homiev2.Mobile.Views;

public partial class EditChorePageView : ContentPage
{

    //HACK the view and the c# code behind is a hack to get this working. After so many different attempts to get this to work. Need to revisit

    private readonly EditChorePageViewModel _viewModel;

    public EditChorePageView(EditChorePageViewModel viewModel)
    {        

        _viewModel = viewModel;
        BindingContext = _viewModel;        
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        Task.Run(async () => await _viewModel.InitializeAsync()).Wait();

        base.OnAppearing();

    }

    private async void SaveUpdatedChore_Clicked(object sender, EventArgs e)
    {

        if (_viewModel.IsAdvancedChore)
        {
            _viewModel.AdvancedChore.DOfWeek = null;
            _viewModel.AdvancedChore.DOfMonth = null;
            _viewModel.AdvancedChore.FirstDOfMonth = null;
            _viewModel.AdvancedChore.LastDOfMonth = null;

            if (dOfWeekRadiobtn.IsChecked == true)
            {
                _viewModel.AdvancedChore.DOfWeek = (DayOfWeek)dOfWeekPicker.SelectedIndex + 1;
            }
            else if (dOfMonthRadiobtn.IsChecked == true)
            {
                _viewModel.AdvancedChore.DOfMonth = (byte)(dOfMonthPicker.SelectedIndex + 1);
            }
            else if (firstDOfMonthRadiobtn.IsChecked == true)
            {
                _viewModel.AdvancedChore.FirstDOfMonth = true;
            }
            else if (lastDOfMonthRadiobtn.IsChecked == true)
            {
                _viewModel.AdvancedChore.LastDOfMonth = true;
            }
        }
        
        await _viewModel.UpdateChoreAsync();
    
    }


}