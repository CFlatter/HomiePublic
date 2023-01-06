using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Homiev2.Mobile.Enum;
using Homiev2.Mobile.Services;
using Homiev2.Shared.Dto;
using Homiev2.Shared.Enums;

namespace Homiev2.Mobile.ViewModels
{
    public partial class AddChorePageViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;

        [ObservableProperty]
        private BaseChoreDto _chore;
        [ObservableProperty]
        private bool _isSimpleChore;
        [ObservableProperty]
        private bool _isAdvancedChore;
        [ObservableProperty]
        private UpdateSimpleChoreDto _simpleChore;
        [ObservableProperty]
        private UpdateAdvancedChoreDto _advancedChore;

        public Array TimeSpanOptions
        {
            get
            {
                return System.Enum.GetValues(typeof(SimpleChoreTimeSpan));
            }
        }

        //public int DaysOfWeekIndex
        //{
        //    get
        //    {
        //        var index = ((int)_advancedChore.DOfWeek) - 1; //HACK - don't like how I am doing this but after multiple attempts seemed to be the only way to pop the view with correct day
        //        return index;
        //    }
        //}

        public string[] DaysOfTheWeek
        {
            get
            {
                string[] daysOfWeek = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
                return daysOfWeek;
            }
        }

        public int[] DaysOfTheMonth
        {
            get
            {
                int[] daysOfMonth = new int[31];
                for (int i = 0; i < daysOfMonth.Length; i++)
                {
                    daysOfMonth[i] = i + 1;
                }
                return daysOfMonth;
            }
        }

        [ObservableProperty]
        private int _dayOfTheMonthIndex; //HACK


        public AddChorePageViewModel(ApiService apiService)
        {
            _apiService = apiService;
            Title = "Add Chore";
        }


        [RelayCommand]
        public async Task SaveChoreAsync()
        {
            IsBusy = true;
            if (_isSimpleChore)
            {
                await _apiService.ApiRequestAsync<UpdateSimpleChoreDto>(ApiRequestType.PATCH, "Chore/UpdateSimpleChore", _simpleChore);
                await Shell.Current.GoToAsync("..");
            }
            else if (_isAdvancedChore)
            {
                if (_advancedChore.IsValid)
                {
                    await _apiService.ApiRequestAsync<UpdateAdvancedChoreDto>(ApiRequestType.PATCH, "Chore/UpdateAdvancedChore", _advancedChore);
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                   await Shell.Current.DisplayAlert("Error", "Please try again", "Dismiss");
                    await Shell.Current.GoToAsync("..");
                }

            }
            IsBusy = false;
        }
    }
}
