using Microsoft.EntityFrameworkCore;
using PeopleDirectory.DataAccess.Entities;

namespace PeopleDirectory.DataAccess
{
    public sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
       : base(options)
        {
        }
        public DbSet<Individual> Individuals { get; set; }

        public DbSet<PhoneNumber> PhoneNumbers { get; set; }

        public DbSet<RelatedIndividual> RelatedIndividuals { get; set; }

        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
