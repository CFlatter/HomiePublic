using System.ComponentModel.DataAnnotations;

namespace Homiev2.Shared.Models
{
    public abstract class BaseFrequency
    {
        [Key]
        public Guid ChoreId { get; set; }

        public abstract DateTime InitNextDueDate(DateTime? dateOfChore = null);
        public abstract DateTime GenerateNextDate(DateTime LastCompletedDate);
    }
}
