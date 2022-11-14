using Homiev2.Shared.Enums;
using Homiev2.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homiev2.Shared.Factories
{
    public class AdvancedChoreFactory : ChoreFactory
    {
        protected override Chore<ChoreFrequencyAdvanced> CreateChore(FrequencyType frequencyType, BaseChore baseChore)
        {

            if (frequencyType == FrequencyType.Advanced)
            {
                Chore<ChoreFrequencyAdvanced> advancedChore = new Chore<ChoreFrequencyAdvanced>();
                return advancedChore;
            }
            else
            {
                throw new Exception();
            }


        }
    }
}
