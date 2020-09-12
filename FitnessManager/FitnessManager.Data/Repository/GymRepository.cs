using EF_HomeWork_4_CORE.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EF_HomeWork_4_CORE
{
    public class GymRepository : Repository<Gym>
    {
        public GymRepository(DbContext context) : base(context) { }
        
    }
}
