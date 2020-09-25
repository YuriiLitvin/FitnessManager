using FitnessManager.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessManager.Data
{
    public class CoachRepository : Repository<Coach>
    {
        public CoachRepository(FitnessDbContext context) : base(context) { }
    }
}

