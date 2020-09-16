using FintessManager.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace FitnessManager.Web.Models
{
    public class CoachModel
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string MobileNumber { get; set; }
        
        [Required]
        [Int32]
        public TypeOfTraining TypeOfTraining { get; set; }

    }
}
