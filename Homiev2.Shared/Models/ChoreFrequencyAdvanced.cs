using Homiev2.Shared.Enums;
using System.Text.Json.Serialization;

namespace Homiev2.Shared.Models
{
    public class ChoreFrequencyAdvanced : BaseFrequency
    {
        [JsonPropertyName("advancedType")]
        public AdvancedType AdvancedType
        {
            get
            {
                if (DOfWeek != null)
                {
                    return AdvancedType.DOfWeek;
                }
                else if (DOfMonth != null)
                {
                    return AdvancedType.DOfMonth;
                }
                else if (FirstDOfMonth == true)
                {
                    return AdvancedType.FirstDOfMonth;
                }
                else if (LastDOfMonth == true)
                {
                    return AdvancedType.LastDOfMonth;
                }

                throw new InvalidDataException();
            }
        }

        [JsonPropertyName("dOfWeek")]
        public DayOfWeek? DOfWeek { get; set; }

        [JsonPropertyName("dOfMonth")]
        public byte? DOfMonth { get; set; }

        [JsonPropertyName("firstDOfMonth")]
        public bool? FirstDOfMonth { get; set; }

        [JsonPropertyName("lastDOfMonth")]
        public bool? LastDOfMonth { get; set; }

        public override DateTime InitNextDueDate(DateTime? dateOfChore = null)
        {
            return GenerateNextDate(dateOfChore ?? DateTime.Now);
        }


        public override DateTime GenerateNextDate(DateTime LastCompletedDate)
        {
            DateTime nextDueDate;

            switch (AdvancedType)
            {
                case AdvancedType.DOfWeek:
                    {
                        if ((((int)DateTime.Now.DayOfWeek - (int)DOfWeek) < 0))
                        {
                            nextDueDate = DateTime.Now.AddDays((int)DOfWeek - (int)DateTime.Now.DayOfWeek);
                        }
                        else
                        {
                            nextDueDate = DateTime.Now.AddDays(7 - ((int)DateTime.Now.DayOfWeek - (int)DOfWeek));
                        }
                        
                        return nextDueDate;

                    }
                case AdvancedType.DOfMonth:
                    {
                        var nextOccurence = DateTime.Now.AddMonths(1);
                        nextDueDate = new DateTime(nextOccurence.Year, nextOccurence.Month, (int)DOfMonth);
                        return nextDueDate;

                    }
                case AdvancedType.FirstDOfMonth:
                    {
                        var nextOccurence = DateTime.Now.AddMonths(1);
                        var firstDayOfMonth = new DateTime(nextOccurence.Year,nextOccurence.Month, 1);
                        nextDueDate = firstDayOfMonth;
                        return nextDueDate;

                    }
                case AdvancedType.LastDOfMonth:
                    {
                        var nextOccurence = DateTime.Now.AddMonths(1);
                        var firstDayOfMonth = new DateTime(nextOccurence.Year, nextOccurence.Month, 1);
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
