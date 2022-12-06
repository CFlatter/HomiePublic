using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homiev2.Shared.Models
{
    public class AuthUser : IdentityUser
    {
  
        public string? FriendlyName { get; set; }
        [Required]
        [NotMapped]
        public string Password { get; set; }
        public HouseholdMember? HouseholdMember { get; set; }
    }
}
