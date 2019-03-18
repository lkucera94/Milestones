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

        public IEnumerable<EventListItem> GetEventsByUserID()
        {
            using (var ctx = new ApplicationDbContext())
            {

                var query = ctx.Events.Where(e => e.UserID == _userId).Select(e => new EventListItem
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
                var entity = ctx.Events.Single(e => e.EventID == model.EventID && e.UserID == _userId);

                entity.EventID = model.EventID;
                entity.UserID = _userId;
                entity.EventName = model.EventName;
                entity.EventLocation = model.EventLocation;
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
                var entity = ctx.Events.Single(e => e.EventID == id && e.UserID == _userId);

                ctx.Events.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        
    }
}
