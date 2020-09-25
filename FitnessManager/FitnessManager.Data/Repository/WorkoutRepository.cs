using FitnessManager.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessManager.Data.Repository
{
    public class WorkoutRepository : Repository<Workout>
    {
        public WorkoutRepository(FitnessDbContext context) : base(context) { }
    }
    
}
