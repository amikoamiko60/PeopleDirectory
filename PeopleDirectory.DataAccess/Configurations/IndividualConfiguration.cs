using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeopleDirectory.DataAccess.Entities;

namespace PeopleDirectory.DataAccess.Configurations
{
    internal sealed class IndividualConfiguration : IEntityTypeConfiguration<Individual>
    {
        public void Configure(EntityTypeBuilder<Individual> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.Gender)
                .IsRequired();

            builder.Property(a => a.PersonalNumber)
                .IsRequired()
                .HasMaxLength(11);

            builder.Property(a => a.BirthDate)
                .IsRequired();

            builder.HasOne(a => a.City)
                .WithMany(c => c.Individuals)
                .HasForeignKey(a => a.CityId)
                .IsRequired();

            builder.Property(a => a.ImagePath)
                .HasMaxLength(250);

            builder.HasMany(a => a.Relationships)
               .WithOne(b => b.Individual)
               .HasForeignKey(c => c.IndividualId);
        }
    }
}
