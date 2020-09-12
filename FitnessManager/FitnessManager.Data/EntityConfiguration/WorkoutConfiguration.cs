using EF_HomeWork_4_CORE.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EF_HomeWork_4_CORE.EntityConfiguration
{
    public class WorkoutConfiguration : IEntityTypeConfiguration<Workout>
    {
        public void Configure(EntityTypeBuilder<Workout> builder)
        {

            builder.Property(w => w.TypeOfTraining).HasConversion<int>();

            builder.Property(w => w.CoachId).IsRequired();

            builder.Property(w => w.GymId).IsRequired();

            builder.Property(w => w.TypeOfTraining).IsRequired();

            builder.Property(w => w.StartTime).IsRequired();

            builder.Property(w => w.FinishTime).IsRequired();

            builder.HasOne(p => p.Coach);
            
            builder.HasOne(p => p.Gym);

            builder.HasData(new {
                Id = 1,
                TypeOfTraining = TypeOfTraining.Yoga,
                CoachId = 4,
                GymId = 1,
                StartTime = new DateTime(2020, 08, 27, 09, 00, 00),
                FinishTime = new DateTime(2020, 08, 27, 10, 00, 00)
            });

            builder.HasData(new {
                Id = 2,
                TypeOfTraining = TypeOfTraining.Fitness,
                CoachId = 2,
                GymId = 2,
                StartTime = new DateTime(2020, 08, 27, 10, 00, 00),
                FinishTime = new DateTime(2020, 08, 27, 11, 00, 00)
            });
            
            builder.HasData(new {
                Id = 3,
                TypeOfTraining = TypeOfTraining.PowerLifting,
                CoachId = 3,
                GymId = 1,
                StartTime = new DateTime(2020, 08, 27, 11, 00, 00),
                FinishTime = new DateTime(2020, 08, 27, 12, 00, 00)
            });
            
            builder.HasData(new {
                Id = 4,
                TypeOfTraining = TypeOfTraining.Dances,
                CoachId = 1,
                GymId = 2,
                StartTime = new DateTime(2020, 08, 27, 12, 00, 00),
                FinishTime = new DateTime(2020, 08, 27, 13, 00, 00)
            });

        }
    }
}
