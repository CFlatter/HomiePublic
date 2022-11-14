using Homiev2.Shared.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Homiev2.Data.Contexts
{
    public class Homiev2Context : IdentityDbContext<AuthUser>
    {
        public Homiev2Context(DbContextOptions<Homiev2Context> options) : base(options)
        {
            Database.EnsureCreated();
        }


        public DbSet<Household> Households { get; set; }
        public DbSet<HouseholdMember> HouseholdMembers { get; set; }
        public DbSet<ChoreLog> ChoreLogs { get; set; }
        public DbSet<BaseChore> Chores { get; set; }
        public DbSet<ChoreFrequencyAdvanced> ChoreFrequencyAdvanced { get; set; }
        public DbSet<ChoreFrequencySimple> ChoreFrequencySimple { get; set; }

    }
}
