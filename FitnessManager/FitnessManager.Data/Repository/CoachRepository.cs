using EF_HomeWork_4_CORE.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EF_HomeWork_4_CORE
{
    public class CoachRepository : Repository<Coach>
    {
        public CoachRepository(DbContext context) : base(context) { }
    }
}
