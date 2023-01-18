using CommunityToolkit.Mvvm.ComponentModel;
using Homiev2.Mobile.Enum;
using Homiev2.Mobile.Models;
using Homiev2.Mobile.Services;
using Homiev2.Mobile.Views;
using Homiev2.Shared.Dto;
using System.Collections.ObjectModel;

namespace Homiev2.Mobile.ViewModels
{
    public partial class PointsPageViewModel : BaseViewModel
    {
        private PointsDto PointsDto { get; set; }
        private readonly ApiService _apiService;
        private readonly HouseholdPageViewModel _householdPageViewModel;

        [ObservableProperty]
        private PointsLeaderboardTimespans selectedTimespan;


        public ObservableCollection<PointsTally> Points { get; set; }



        public Array TimeSpanOptions
        {
            get
            {
                return System.Enum.GetValues(typeof(PointsLeaderboardTimespans));
            }
        }

        public PointsPageViewModel(ApiService apiService, HouseholdPageViewModel householdPageViewModel)
        {
            Title = "Points";
            _apiService = apiService;
            _householdPageViewModel = householdPageViewModel;
            Points = new();
        }

        public async Task GetPointsAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                if (Points.Count != 0)
                {
                    Points.Clear();
                }

                PointsDto pointsDto = new PointsDto();
                switch (selectedTimespan)
                {
                    case PointsLeaderboardTimespans.ThisWeek:
                        pointsDto.StartDate = GetStartOfWeekDate(DateTime.Now);
                        pointsDto.EndDate = DateTime.Now;
                        break;
                    case PointsLeaderboardTimespans.LastWeek:
                        pointsDto.StartDate = GetStartOfWeekDate(DateTime.Now).AddDays(-7);
                        pointsDto.EndDate = pointsDto.StartDate.AddDays(6);
                        break;
                    case PointsLeaderboardTimespans.ThisMonth:
                        pointsDto.StartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                        pointsDto.EndDate = DateTime.Now;
                        break;
                    case PointsLeaderboardTimespans.LastMonth:
                        pointsDto.StartDate = new DateTime(DateTime.Today.Year, DateTime.Today.AddMonths(-1).Month, 1);
                        pointsDto.EndDate = pointsDto.StartDate.AddMonths(1).AddDays(-1);
                        break;
                    case PointsLeaderboardTimespans.AllTime:
                        pointsDto.StartDate = DateTime.MinValue;
                        pointsDto.EndDate = DateTime.Now;
                        break;
                    default:
                        break;
                }


                var choreLogs = await _apiService.ApiRequestAsync<List<ChoreLogDto>>(ApiRequestType.GET, $"Points/Points?StartDate={pointsDto.StartDate.ToString()}&EndDate={pointsDto.EndDate.ToString()}");

                if (choreLogs != null)
                {
                    foreach (var householdMember in choreLogs.GroupBy(x => x.HouseholdMemberId))
                    {

                        PointsTally individualPoints = new();
                        
                        individualPoints.Points = householdMember.Sum(x => x.Points);

                        individualPoints.MemberName = _householdPageViewModel.HouseholdMembers.Where(x => x.HouseholdMemberId == householdMember.Select(x => x.HouseholdMemberId).First()).Select(y => y.MemberName).First();

                        Points.Add(individualPoints);


                    }
                }



            }
            catch (UnauthorizedAccessException)
            {
                await Shell.Current.DisplayAlert("Please login", "login has expired", "Dismiss");
                await Shell.Current.GoToAsync($"{nameof(LoginPageView)}");
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Error", e.Message, "Dismiss");
            }
            finally
            {
                IsBusy = false;
            }

        }

        private static DateTime GetStartOfWeekDate(DateTime today)
        {
            int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
            return today.AddDays(-1 * diff).Date;
        }
    }
}
