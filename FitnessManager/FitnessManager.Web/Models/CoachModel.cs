using FintessManager.Data;
using FintessManager.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace FitnessManager.Web.Models
{
    public class CoachModel 
    {
        
        public string FirstName { get; set; }

        [Required, StringLength(255)]
        public string LastName { get; set; }

        [Required, StringLength(255), EmailAddress]
        public string Email { get; set; }

        
        [Required, StringLength(255), Phone]
        public string MobileNumber { get; set; }
        
        
        [Required, EnumDataType(typeof(TypeOfTraining))]
        public TypeOfTraining TypeOfTraining { get; set; }

    }
}
