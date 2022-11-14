using Homiev2.Shared.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Homiev2.Shared.Models
{
    public class BaseChore
    {
        [Key]
        public Guid ChoreId { get; set; }
        public string TaskName { get; set; }
        public byte Points { get; set; }
        public FrequencyType FrequencyTypeId { get; set; }
        public Guid HouseholdId { get; set; }
        public DateTime? LastCompletedDate { get; protected set; }
        public DateTime NextDueDate { get; protected set; }
        public string? CreatedBy { get; set; }

        public void InitNextDueDate(DateTime? dateOfChore = null)
        {
            NextDueDate = dateOfChore.HasValue ? dateOfChore.Value : DateTime.Now;
        }

    }
}
