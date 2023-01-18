using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homiev2.Mobile.Enum
{
    public enum PointsLeaderboardTimespans
    {
        [Display(Name = "This Week")]
        ThisWeek,
        [Display(Name = "Last Week")]
        LastWeek,
        [Display(Name = "This Month")]
        ThisMonth,
        [Display(Name = "Last Month")]
        LastMonth,
        [Display(Name = "All Time")]
        AllTime

    }
}
