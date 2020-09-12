using System;
using System.Collections.Generic;
using System.Text;

namespace EF_HomeWork_4_CORE.Entity
{
    public class Workout : BaseEntity
    {
        
        public TypeOfTraining TypeOfTraining { get; set; }
        public int CoachId { get; set; }
        public int GymId { get; set; }
        public Coach Coach { get; set; }
        public Gym Gym { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
    }

    
}
