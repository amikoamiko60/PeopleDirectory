using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeopleDirectory.DataAccess.Entities;

namespace PeopleDirectory.DataAccess.Configurations
{
    internal sealed class RelatedIndividualConfiguration : IEntityTypeConfiguration<RelatedIndividual>
    {
        public void Configure(EntityTypeBuilder<RelatedIndividual> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.RelationshipType)
                .HasConversion<int>()
                .IsRequired();

            builder.HasOne(a => a.Individual)
                .WithMany(a => a.Relationships)
                .HasForeignKey(a => a.IndividualId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(a => a.RelatedPerson)
                .WithMany()
                .HasForeignKey(a => a.RelatedIndividualId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
