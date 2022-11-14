using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Homiev2.Shared.Models
{
    public abstract class BaseFrequency
    {
        [Key]
        public Guid ChoreId { get; set; }

        public abstract DateTime GenerateNextDate(DateTime LastCompletedDate);
    }
}
