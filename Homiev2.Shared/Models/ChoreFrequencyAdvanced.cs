using System.Text.Json.Serialization;

namespace Homiev2.Shared.Models
{
    public class ChoreFrequencyAdvanced : BaseFrequency
    {
        [JsonPropertyName("advancedType")]
        public byte AdvancedType { get; set; }

        [JsonPropertyName("dOfWeek")]
        public int? DOfWeek { get; set; }

        [JsonPropertyName("dOfMonth")]
        public byte? DOfMonth { get; set; }

        [JsonPropertyName("firstDOfMonth")]
        public bool? FirstDOfMonth { get; set; }

        [JsonPropertyName("lastDOfMonth")]
        public bool? LastDOfMonth { get; set; }

        public override DateTime GenerateNextDate(DateTime LastCompletedDate)
        {
            DateTime nextDueDate;

            switch (AdvancedType)
            {
                case 1:
                    {
                        //DayOfWeek
                        nextDueDate = LastCompletedDate.AddDays(7);
                        return nextDueDate;

                    }
                case 2:
                    {
                        //DayOfMonth
                        nextDueDate = LastCompletedDate.AddMonths(1);
                        return nextDueDate;

                    }
                case 3:
                    {
                        //FirstDayOfMonth
                        var year = LastCompletedDate.Year;
                        var nextMonth = LastCompletedDate.AddMonths(1).Month;
                        var firstDayOfMonth = new DateTime(year, nextMonth, 1);
                        nextDueDate = firstDayOfMonth;
                        return nextDueDate;

                    }
                case 4:
                    {
                        //LastDayOfMonth
                        var year = LastCompletedDate.Year;
                        var nextMonth = LastCompletedDate.AddMonths(1).Month;
                        var firstDayOfMonth = new DateTime(year, nextMonth, 1);
                        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                        nextDueDate = lastDayOfMonth;
                        return nextDueDate;

                    }
                default:
                    {
                        return nextDueDate = DateTime.Now;
                    }
            }


        }
    }
}
