using Homiev2.Mobile.ViewModels;

namespace Homiev2.Mobile.Views;


public partial class AddChorePageView : ContentPage
{

    //HACK the view and the c# code behind is a hack to get this working. After so many different attempts to get this to work. Need to revisit

    private readonly AddChorePageViewModel _viewModel;

    public AddChorePageView(AddChorePageViewModel viewModel)
    {        

        _viewModel = viewModel;
        BindingContext = _viewModel;        
        InitializeComponent();
        datePicker.MinimumDate = DateTime.Now;
    }


    private async void SaveChore_Clicked(object sender, EventArgs e)
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
        
        await _viewModel.SaveChoreAsync();
    
    }

    private void AdvancedOptions_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        _viewModel.IsAdvancedChore = _viewModel.IsAdvancedChore ? false : true;
        _viewModel.IsSimpleChore = _viewModel.IsAdvancedChore ? false : true;
    }
}