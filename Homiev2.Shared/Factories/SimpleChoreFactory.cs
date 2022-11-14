using Homiev2.Shared.Enums;
using Homiev2.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homiev2.Shared.Factories
{
    public class SimpleChoreFactory : ChoreFactory
    {
        protected override Chore<ChoreFrequencySimple> CreateChore(FrequencyType frequencyType, BaseChore baseChore)
        {

            if (frequencyType == FrequencyType.Simple)
            {
                Chore<ChoreFrequencySimple> simpleChore = new()
                {
                    ChoreId = baseChore.ChoreId,
                    TaskName = baseChore.TaskName,
                    Points = baseChore.Points,
                    FrequencyTypeId = baseChore.FrequencyTypeId,
                    HouseholdId = baseChore.HouseholdId,
                    CreatedBy = baseChore.CreatedBy,
                    Schedule = new ChoreFrequencySimple()
                };
            
                return simpleChore;
            }
            else
            {
                throw new Exception();
            }


        }
    }
}
