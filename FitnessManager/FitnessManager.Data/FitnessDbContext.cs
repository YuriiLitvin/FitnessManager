using EF_HomeWork_4_CORE.Entity;
using EF_HomeWork_4_CORE.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EF_HomeWork_4_CORE
{
    public class FitnessDbContext : DbContext
    {
        public  DbSet<Coach> Coaches { get; set; }

        public DbSet<Gym> Gyms { get; set; }

        public DbSet<Workout> Workouts { get; set; }

        //public FitnessDbContext()
        //{
        //    //Database.EnsureCreated();
        //}

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
