using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homiev2.Shared.Dto
{
    public class HouseholdDTO
    {
        [Required]
        [MaxLength(20)]
        public string HouseholdName { get; set; }
    }
}
