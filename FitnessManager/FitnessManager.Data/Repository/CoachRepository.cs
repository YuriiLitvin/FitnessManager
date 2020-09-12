using FintessManager.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FintessManager.Data
{
    public class CoachRepository : Repository<Coach>
    {
        public CoachRepository(DbContext context) : base(context) { }
    }
}

