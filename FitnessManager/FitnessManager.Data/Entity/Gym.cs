using System;
using System.Collections.Generic;
using System.Text;

namespace EF_HomeWork_4_CORE.Entity
{
    public class Gym : BaseEntity
    {

        public string Title { get; set; }

        public int TrainingPeolpeCount { get; set; }
    
        public List<Workout> Workouts { get; set; }
    }
}
