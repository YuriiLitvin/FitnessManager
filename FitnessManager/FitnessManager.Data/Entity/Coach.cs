using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace FitnessManager.Data.Entity
{
    public class Coach : BaseEntity, IEquatable<Coach>
    {
        public string  FirstName { get; set; }
        
        public string  LastName { get; set; }
        
        public string  Email { get; set; }
        
        public string  MobileNumber { get; set; }
        
        public TypeOfTraining TypeOfTraining { get; set; }
        
        public List<Workout> Workouts { get; set; }

        public bool Equals(Coach other)
        {
            if (other == null)
                return false;

            if (this.FirstName == other.FirstName &&
                this.LastName == other.LastName &&
                this.Email == other.Email &&
                this.MobileNumber == other.MobileNumber &&
                this.TypeOfTraining == other.TypeOfTraining)
                return true;
            else
                return false;
        }
        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            Coach coachObj = obj as Coach;

            if (coachObj == null)
                return false;
            else
                return Equals(coachObj);
        }
    }
}
