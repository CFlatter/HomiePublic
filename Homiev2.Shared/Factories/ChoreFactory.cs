using Homiev2.Shared.Enums;
using Homiev2.Shared.Interfaces.Factories;
using Homiev2.Shared.Models;

namespace Homiev2.Shared.Factories
{
    public abstract class ChoreFactory : IChoreFactory
    {
        protected abstract BaseChore CreateChore(FrequencyType frequencyType, BaseChore baseChore);

        public BaseChore GetChore(FrequencyType frequencyType, BaseChore baseChore)
        {
            var chore = CreateChore(frequencyType, baseChore);

            return chore;
        }

    }
}
