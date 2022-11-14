using Homiev2.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homiev2.Shared.Models
{
    public class Chore<T> : BaseChore where T : BaseFrequency
    {

        public T Schedule { get; set; }

        public void GenerateNewChore(DateTime completedDateTime)
        {
            LastCompletedDate = completedDateTime;
            NextDueDate = Schedule.GenerateNextDate((DateTime)LastCompletedDate);

        }


    }
}
