using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milestones.Data
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }

        [Required]
        public Guid UserID { get; set; }

        [Required]
        public string EventName { get; set; }

        [Required]
        [Display(Name = "Event Date"), DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateOfEvent { get; set; }

        public string EventComment { get; set; }

        [Required]
        public string KidAgeAtEvent { get; set; }

        public string EventLocation { get; set; }

        [Required]
        public int KidID { get; set; }

        public virtual Kid Kids { get; set; }
    }
}
