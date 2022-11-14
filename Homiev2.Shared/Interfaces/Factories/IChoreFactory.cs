using Homiev2.Shared.Enums;
using Homiev2.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homiev2.Shared.Interfaces.Factories
{
    public interface IChoreFactory
    {
        BaseChore GetChore(FrequencyType frequencyType, BaseChore baseChore);
    }
}
