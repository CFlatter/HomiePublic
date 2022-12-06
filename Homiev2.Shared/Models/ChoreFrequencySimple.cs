using System.Text.Json.Serialization;

namespace Homiev2.Shared.Models
{
    public class ChoreFrequencySimple : BaseFrequency
    {
        [JsonPropertyName("timeSpan")]
        public short TimeSpan { get; set; } //Day, Week, Month or year (e.g. 1 for Day, 7 for week, 30 for Month, 365 for year)

        [JsonPropertyName("multiplier")]
        public short Multiplier { get; set; } //How often it should occur, e.g. if Timespan is set to 1 for Day. If you wanted the task to occur every 3 days set Multiplier to 3

        public override DateTime GenerateNextDate(DateTime LastCompletedDate)
        {
            DateTime NextDueDate = LastCompletedDate.AddDays(TimeSpan * Multiplier);
            return NextDueDate.Date;
        }

    }
}
