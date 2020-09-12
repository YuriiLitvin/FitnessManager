using EF_HomeWork_4_CORE.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EF_HomeWork_4_CORE.Repository
{
    class WorkoutRepository : Repository<Workout>
    {
        public WorkoutRepository(DbContext context) : base(context) { }
    }
    
}
