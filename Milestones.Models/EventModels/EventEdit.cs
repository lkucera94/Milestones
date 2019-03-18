using Milestones.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milestones.Models.EventModels
{
    public class EventEdit
    {
        public int EventID { get; set; }
         
        public Guid UserID { get; set; }

        [Display(Name = "Child")]
        public int KidID { get; set; }


        [Display(Name = "Event")]
        public string EventName { get; set; }

        [Display(Name = "Event Location")]
        public string EventLocation { get; set; }

        public DateTime DateOfEvent { get; set; }

        [Display(Name = "Comments")]
        public string EventComment { get; set; }


        //[Display(Name = "Child's Age")]
        //public string KidAgeAtEvent { get; set; }

        public virtual Kid Kids { get; set; } 
    }
}
