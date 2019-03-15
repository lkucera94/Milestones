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
            
                
                
                
                
                //{
            //    UserID = _userID,
            //    FName = model.FName,
            //    LName = model.LName,
            //    DOB = model.DOB,
            //    About = model.About,
            //    Gender = model.Gender,
            //};
            
          //  ctx.SaveChanges();
            //var kidID = ctx.Kids.OrderByDescending(p => p.KidID).FirstOrDefault().KidID;
            //CalculateAge(kidID);


        }

        public IEnumerable<KidGetKid> GetKidsByUserID(Guid userID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.
                    Kids.
                    Where(k => k.UserID == userID).
                    Select(k => new KidGetKid
                    {
                        KidID = k.KidID,
                        UserID = k.UserID,
                        FName = k.FName,
                        LName = k.LName,
                        DOB = k.DOB,
                        Age = k.Age,
                        About = k.About,
                        Gender = k.Gender,

                    }).ToArray();

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
                    UserID = entity.UserID,
                    FName = entity.FName,
                    LName = entity.LName,
                    DOB = entity.DOB,
                    Age = entity.Age,
                    About = entity.About,
                    Gender = entity.Gender,

                };

                //CalculateAge(kidID);

                return model;
            }
        }

        public bool EditKid(KidEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Kids.Single(k => k.KidID == model.KidID);

                entity.KidID = model.KidID;
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
                var entity = ctx.Kids.Single(k => k.KidID == kidID);

                ctx.Kids.Remove(entity);

                return (ctx.SaveChanges() == 1);
            }
        }

        //private bool CalculateAge(int kidID)
        //{
        //    var ctx = new ApplicationDbContext();
        //    var kid = ctx.Kids.Single(b => b.KidID == kidID);

        //    DateTime dob = kid.DOB;
        //    var age = CalcKidsAge(dob);

        //    string CalcKidsAge(DateTime Dob)
        //    {
        //        DateTime Now = DateTime.Now;
        //        int years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
        //        DateTime PastYearDate = Dob.AddYears(years);
        //        int months = 0;
        //        for (int i = 1; i <= 12; i++)
        //        {
        //            if (PastYearDate.AddMonths(i) == Now)
        //            {
        //                months = i;
        //                break;
        //            }
        //            else if (PastYearDate.AddMonths(i) >= Now)
        //            {
        //                months = i - 1;
        //                break;
        //            }
        //        }
        //        int days = Now.Subtract(PastYearDate.AddMonths(months)).Days;

        //        age = $"Age: {years} Year(s) {months} Months(s) {days} Day(s)";

        //        kid.Age = age;

        //        return age;
        //    }
        //    return ctx.SaveChanges() == 1;
        //}

    }
}

