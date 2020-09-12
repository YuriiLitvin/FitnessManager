using System;
using System.Collections.Generic;
using System.Text;

namespace EF_HomeWork_4_CORE.Entity
{
    public class Coach : BaseEntity
    {
        public string  FirstName { get; set; }
        
        public string  LastName { get; set; }
        
        public string  Email { get; set; }
        
        public string  MobileNumber { get; set; }
        
        public TypeOfTraining TypeOfTraining { get; set; }
        
        public List<Workout> Workouts { get; set; }

    }
}
