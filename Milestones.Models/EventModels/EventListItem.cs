using Milestones.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milestones.Models.EventModels
{
    public class EventListItem
    {
        public int EventID { get; set; }

        public int KidID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Event")]
        public string EventName { get; set; }

        [Display(Name = "Age")]
        public string KidAgeAtEvent { get; set; }

        public virtual Kid Kids { get; set; }
    }
}
