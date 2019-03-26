using Milestones.Data;
using Milestones.Models.KidModels;
using Milestones.Models.PageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milestones.Services
{
    public class KidService
    {
        private readonly Guid _userID;

        public KidService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreateKid(KidCreate model)
        {
            var kid = new Kid(_userID, model.FName, model.LName, model.DOB, model.About, model.Gender);
                
            var ctx = new ApplicationDbContext();
            ctx.Kids.Add(kid);
            return ctx.SaveChanges() == 1;
          
        }

        public IEnumerable<KidGetKid> GetKidsByUserID()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.
                    Kids.
                    Where(k => k.UserID == _userID).
                    Select(k => new KidGetKid
                    {
                        KidID = k.KidID,
                        UserID = _userID,
                        FName = k.FName,
                        LName = k.LName,
                        DOB = k.DOB,
                        Age = k.Age,
                        About = k.About,
                        Gender = k.Gender,

                    }).ToArray();

                foreach(var kid in query)
                {
                    kid.Age = CalcKidsAge(kid.DOB);
                }

                return query;
            }
        }

        public KidDetail GetKidByKidID(int kidID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Kids.Single(k => k.KidID == kidID);

                var model = new KidDetail
                {
                    KidID = entity.KidID,
                    UserID = _userID,
                    FName = entity.FName,
                    LName = entity.LName,
                    DOB = entity.DOB,
                    Age = CalcKidsAge(entity.DOB),
                    About = entity.About,
                    Gender = entity.Gender,

                };
                
                return model;
            }
        }

        public bool EditKid(KidEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Kids.Single(k => k.KidID == model.KidID && k.UserID == _userID);

                entity.UserID = _userID;
                entity.KidID = model.KidID;
                entity.DOB = model.DOB;
                entity.FName = model.FName;
                entity.LName = model.LName;
                entity.About = model.About;
                entity.Gender = model.Gender;

                return (ctx.SaveChanges() == 1);
            }
        }

        public bool DeleteKid(int kidID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.
                             Kids.
                             Single(k => k.KidID == kidID);

                var query =
                    from eventobject in ctx.Events
                    where
                            ctx.
                            Events.
                            All(e => e.KidID == kidID)
                    orderby eventobject.EventID
                    select eventobject;
                foreach (Event eventToDelete in query)
                    ctx.Events.Remove(eventToDelete);

                ctx.Kids.Remove(entity);

                return (ctx.SaveChanges() == 1);
            }
        }

        private string CalcKidsAge(DateTime Dob)
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

            var age = $" {years} Year(s) {months} Months(s) {days} Day(s)";

            return age;
        }


    }
}

