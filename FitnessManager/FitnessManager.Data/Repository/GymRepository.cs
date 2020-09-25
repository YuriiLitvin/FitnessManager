using FitnessManager.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessManager.Data
{
    public class GymRepository : Repository<Gym>
    {
        public GymRepository(FitnessDbContext context) : base(context) { }
        
    }
}
