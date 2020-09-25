using FitnessManager.Data.Entity;
using FitnessManager.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;


namespace FitnessManager.Data
{
    public class FitnessDbContext : DbContext
    {
        public FitnessDbContext(DbContextOptions<FitnessDbContext> options) : base(options) { }
        
        
        public  DbSet<Coach> Coaches { get; set; }

        public DbSet<Gym> Gyms { get; set; }

        public DbSet<Workout> Workouts { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CoachConfiguration());
            modelBuilder.ApplyConfiguration(new GymConfiguration());
            modelBuilder.ApplyConfiguration(new WorkoutConfiguration());

        }
    }
}
