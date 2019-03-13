using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milestones.Models.KidModels
{
    public class KidDetail
    {
        [Key]
        public int KidID { get; set; }


        public Guid UserID { get; set; }


        [Display(Name = "First Name")]
        public string FName { get; set; }

        [Display(Name = "Last Name")]
        public string LName { get; set; }


        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }


        public int Age { get; set; }

        [Display(Name = "Additional Info")]
        public string About { get; set; }

        public string Gender { get; set; }
    }
}
