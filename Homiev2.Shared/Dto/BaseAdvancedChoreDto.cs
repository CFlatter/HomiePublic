using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Homiev2.Shared.Dto
{
    public abstract class BaseAdvancedChoreDto
    {

        abstract public DayOfWeek? DOfWeek { get; set; }
        abstract public byte? DOfMonth { get; set; }
        abstract public bool? FirstDOfMonth { get; set; }
        abstract public bool? LastDOfMonth { get; set; }

        public bool IsValid => String.IsNullOrEmpty(ValidationMessage);

        public string ValidationMessage
        {
            get
            {
                List<object> properties = new();
                properties.Add(DOfWeek);
                properties.Add(DOfMonth);
                properties.Add(FirstDOfMonth);
                properties.Add(LastDOfMonth);

                if (properties.Where(x => x != null).Count() > 1)
                {
                    return "Multiple date options provided. Only 1 can be chosen";
                }
                else if (properties.Where(x => x != null).Count() == 0)
                {
                    return "Please choose a date option";
                }

                return string.Empty;

            }
        }
    }
}
