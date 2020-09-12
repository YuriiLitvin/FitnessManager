using EF_HomeWork_4_CORE.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace EF_HomeWork_4_CORE.EntityConfiguration
{
    public class CoachConfiguration : IEntityTypeConfiguration<Coach>
    {
        public void Configure(EntityTypeBuilder<Coach> builder)
        {
            builder.Property(p => p.FirstName).HasMaxLength(255).IsRequired();
            
            builder.Property(p => p.LastName).HasMaxLength(255).IsRequired();

            builder.Property(p => p.MobileNumber).HasMaxLength(255).IsRequired();

            builder.Property(p => p.Email).HasMaxLength(255).IsRequired();

            builder.Property(p => p.TypeOfTraining).HasConversion<int>();


            builder.HasData(new {
                Id = 1,
                FirstName = "Petrovich",
                LastName = "",
                Email = "petrovich@gmail.com",
                MobileNumber = "09923",
                TypeOfTraining = TypeOfTraining.Dances
            });

            builder.HasData(new {
                Id = 2,
                FirstName = "Samson",
                LastName = "",
                Email = "samson@gmail.com",
                MobileNumber = "097325",
                TypeOfTraining = TypeOfTraining.Fitness
            });
            
            builder.HasData (new {
                Id = 3,
                FirstName = "Oleksandr",
                LastName = "I",
                Email = "sashkapower@gmail.com",
                MobileNumber = "09544234",
                TypeOfTraining = TypeOfTraining.PowerLifting
            });
            
            builder.HasData (new {
                Id = 4,
                FirstName = "Anna",
                LastName = "G.",
                Email = "g_anna@gmail.com",
                MobileNumber = "0930954",
                TypeOfTraining = TypeOfTraining.Yoga
            });

            

        }
    }
}
