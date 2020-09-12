using EF_HomeWork_4_CORE.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace EF_HomeWork_4_CORE.EntityConfiguration
{
    public class GymConfiguration : IEntityTypeConfiguration<Gym>
    {
        public void Configure(EntityTypeBuilder<Gym> builder)
        {
            builder.Property(g => g.Title).HasMaxLength(255).IsRequired();

            builder.Property(g => g.TrainingPeolpeCount).IsRequired();

            builder.HasData(new { Id = 1, Title = "Sparta", TrainingPeolpeCount = 100 });

            builder.HasData (new { Id = 2, Title = "FitnessLife", TrainingPeolpeCount = 150 });

        }
    }
}
