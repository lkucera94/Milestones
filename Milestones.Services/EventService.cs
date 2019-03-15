using Milestones.Data;
using Milestones.Models.EventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milestones.Services
{
    public class EventService
    {
        private readonly Guid _userId;
        public EventService(Guid userId)
        {
            _userId = userId;
        }
        public EventService() { }

        public bool CreateEvent(EventCreate model)
        {
            var newEvent = new Event(_userId, model.KidID,model.EventName, model.EventLocation, model.DateOfEvent, model.EventComment);

            var ctx = new ApplicationDbContext();
            ctx.Events.Add(newEvent);
            return ctx.SaveChanges() == 1;
        }

        public IEnumerable<EventListItem> GetEventsByUserID(Guid id)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var query = ctx.Events.Where(e => e.UserID == id).Select(e => new EventListItem
                {
                    KidID = e.KidID,
                    EventID = e.EventID,
                    EventName = e.EventName,
                    FirstName = e.Kids.FName,
                    LastName = e.Kids.LName,
                    KidAgeAtEvent = e.KidAgeAtEvent

                });
                return query.ToArray();
            }
        }

        public IEnumerable<EventListItem> GetEventsByKidID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Events.Where(e => e.KidID == id).Select(e => new EventListItem
                {
                    KidID = e.KidID,
                    EventID = e.EventID,
                    EventName = e.EventName,
                    FirstName = e.Kids.FName,
                    LastName = e.Kids.LName,
                    KidAgeAtEvent = e.KidAgeAtEvent
                });
                return query.ToArray();
            }
        }

        public EventDetail GetEventByEventID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Events.FirstOrDefault(e => e.EventID == id);
                var model = new EventDetail()
                {
                    EventID = entity.EventID,
                    EventName = entity.EventName,
                    EventLocation = entity.EventLocation,
                    DateOfEvent = entity.DateOfEvent,
                    FirstName = entity.Kids.FName,
                    LastName = entity.Kids.LName,
                    KidAgeAtEvent = entity.KidAgeAtEvent,
                    Gender = entity.Kids.Gender,
                    EventComment = entity.EventComment
                };
                return model;
            }
        }

        public bool EditEvent(EventEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Events.Single(e => e.EventID == model.EventID);

                entity.EventID = model.EventID;
                entity.EventName = model.EventName;
                entity.EventLocation = model.EventLocation;
                entity.KidAgeAtEvent = model.KidAgeAtEvent;
                entity.KidID = model.KidID;
                entity.EventComment = model.EventComment;
                entity.DateOfEvent = model.DateOfEvent;

                return ctx.SaveChanges() == 1;

            }
        }

        public bool DeleteEvent(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Events.Single(e => e.EventID == id);

                ctx.Events.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        //private bool CalculateAge(int kidID, int eventID)
        //{
        //    var ctx = new ApplicationDbContext();
        //    var kid = ctx.Kids.Single(b => b.KidID == kidID);
        //    var milestone = ctx.Events.Single(m => m.EventID == eventID);

        //    DateTime dob = kid.DOB;
        //    var age = CalcKidsAgeAtEvent(dob);

        //    string CalcKidsAgeAtEvent(DateTime Dob)
        //    {
        //        DateTime Event = milestone.DateOfEvent;
        //        int years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
        //        DateTime PastYearDate = Dob.AddYears(years);
        //        int months = 0;
        //        for (int i = 1; i <= 12; i++)
        //        {
        //            if (PastYearDate.AddMonths(i) == Event)
        //            {
        //                months = i;
        //                break;
        //            }
        //            else if (PastYearDate.AddMonths(i) >= Event)
        //            {
        //                months = i - 1;
        //                break;
        //            }
        //        }
        //        int days = Event.Subtract(PastYearDate.AddMonths(months)).Days;

        //        age = $"Age: {years} Year(s) {months} Months(s) {days} Day(s)";

        //        milestone.KidAgeAtEvent = age;

        //        return age;
        //    }
        //    return ctx.SaveChanges() == 1;
        //}
    }
}
