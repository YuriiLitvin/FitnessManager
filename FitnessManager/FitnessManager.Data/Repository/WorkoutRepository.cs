using FintessManager.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FintessManager.Data.Repository
{
    public class WorkoutRepository : Repository<Workout>
    {
        public WorkoutRepository(DbContext context) : base(context) { }
    }
    
}
