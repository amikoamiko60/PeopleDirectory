using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeopleDirectory.DataAccess.Entities;

namespace PeopleDirectory.DataAccess.Configurations
{
    internal sealed class PhoneNumberConfiguration : IEntityTypeConfiguration<PhoneNumber>
    {
        public void Configure(EntityTypeBuilder<PhoneNumber> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Type)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(a => a.Number)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(a => a.Individual)
                .WithMany(a => a.PhoneNumbers)
                .HasForeignKey(a => a.IndividualId)
                .IsRequired();
        }
    }
}
