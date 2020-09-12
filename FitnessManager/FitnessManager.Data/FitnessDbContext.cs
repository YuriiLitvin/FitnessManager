using FintessManager.Data.Entity;
using FintessManager.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FintessManager.Data
{
    public class FitnessDbContext : DbContext
    {
        public  DbSet<Coach> Coaches { get; set; }

        public DbSet<Gym> Gyms { get; set; }

        public DbSet<Workout> Workouts { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            var connectingString = @"Data Source=DESKTOP-S7BNTGV; 
                                     Initial Catalog=FitnessDb; 
                                     Integrated Security=True";
            
            optionsBuilder.UseSqlServer(connectingString);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CoachConfiguration());
            modelBuilder.ApplyConfiguration(new GymConfiguration());
            modelBuilder.ApplyConfiguration(new WorkoutConfiguration());

        }
    }
}
