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

        public Event()
        {

        }

        public Event(Guid userId, int kidID,  string eventName, string eventLocation, DateTime dateOfEvent, string eventComment)
        {
            UserID = userId;
            KidID = kidID;
            EventName = eventName;
            EventLocation = eventLocation;
            DateOfEvent = dateOfEvent;
            EventComment = eventComment;
            KidAgeAtEvent = CalculateAge(KidID);
        }

        string CalculateAge(int kidID)
        {
            var ctx = new ApplicationDbContext();
            var kid = ctx.Kids.Single(b => b.KidID == kidID);
            

            DateTime dob = kid.DOB;
            var age = CalcKidsAgeAtEvent(dob);

            string CalcKidsAgeAtEvent(DateTime Dob)
            {
                DateTime Event = DateOfEvent;
                int years = new DateTime(DateOfEvent.Subtract(Dob).Ticks).Year - 1;
                DateTime PastYearDate = Dob.AddYears(years);
                int months = 0;
                for (int i = 1; i <= 12; i++)
                {
                    if (PastYearDate.AddMonths(i) == Event)
                    {
                        months = i;
                        break;
                    }
                    else if (PastYearDate.AddMonths(i) >= Event)
                    {
                        months = i - 1;
                        break;
                    }
                }
                int days = Event.Subtract(PastYearDate.AddMonths(months)).Days;

                age = $"Age: {years} Year(s) {months} Months(s) {days} Day(s)";

                KidAgeAtEvent = age;

                return age;
            }
            return age;
        }
    }            
}
