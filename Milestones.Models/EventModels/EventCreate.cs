using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milestones.Models.EventModels
{
    public class EventCreate
    {
        [Required]
        [Display(Name = "Child")]
        public int KidID { get; set; }

        [Required]
        [Display(Name = "Event")]
        public string EventName { get; set; }

        [Required]
        public DateTime DateOfEvent { get; set; }

        [Display(Name = "Comments")]
        public string EventComment { get; set; }

        //[Required]
        //[Display(Name = "Child's Age")]
        //public string KidAgeAtEvent { get; set; }

        [Display(Name = "Event Location")]
        public string EventLocation { get; set; }
    }
}
