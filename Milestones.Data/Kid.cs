using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milestones.Data
{
    public class Kid
    {
        [Key]
        public int KidID { get; set; }

        [Required]
        public Guid UserID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FName { get; set; }

        [Display(Name = "Last Name")]
        public string LName { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }

        //[Required]
        public string Age { get; set; }

        [Display(Name = "Additional Info"), DisplayFormat(DataFormatString = "{0:d}")]
        public string About { get; set; }

        public string Gender { get; set; }

        public Kid()
        {

        }

        public Kid(Guid userId, string first, string last, DateTime dob, string about, string gender)
        {
            UserID = userId;
            FName = first;
            LName = last;
            DOB = dob;
            About = about;
            Gender = gender;
            Age = CalcKidsAge(dob);
        }

        string CalcKidsAge(DateTime Dob)
        {
            DateTime Now = DateTime.Now;
            int years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
            DateTime PastYearDate = Dob.AddYears(years);
            int months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    months = i - 1;
                    break;
                }
            }
            int days = Now.Subtract(PastYearDate.AddMonths(months)).Days;

            var age = $"Age: {years} Year(s) {months} Months(s) {days} Day(s)";

            return age;
        }
    }
}
