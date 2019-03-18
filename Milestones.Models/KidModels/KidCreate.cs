using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milestones.Models.KidModels
{
    public class KidCreate
    {
        //[Required]
        //public Guid UserID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FName { get; set; }

        [Display(Name = "Last Name")]
        public string LName { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }

        //[Required]
        //public string Age { get; set; }

        [Display(Name = "Additional Info")]
        public string About { get; set; }

        public string Gender { get; set; }
    }
}
