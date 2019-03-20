using Microsoft.AspNet.Identity;
using Milestones.Models.EventModels;
using Milestones.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Milestones.API.Controllers
{
    [RoutePrefix("api/Event")]
    public class EventController : ApiController
    {
        [Route("All")]
        public IHttpActionResult GetAll()
        {
            var service = GetEventService();
            var events = service.GetEventsByUserID();
            return Ok(events);
        }

        [Route("ByEvent/{id:int}")]
        public IHttpActionResult GetByEventID(int id)
        {
            var service = GetEventService();
            var events = service.GetEventByEventID(id);
            return Ok(events);
        }

        [Route("ByKid/{id:int}")]
        public IHttpActionResult GetByKidID(int id)
        {
            var service = GetEventService();
            var events = service.GetEventsByKidID(id);
            return Ok(events);
        }

        [Route("Create")]
        public IHttpActionResult Post(EventCreate create)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = GetEventService();

            if (!service.CreateEvent(create))
                return InternalServerError();

            return Ok();
        }

        [Route("Edit")]
        public IHttpActionResult Put(EventEdit edit)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = GetEventService();

            if (!service.EditEvent(edit))
                return InternalServerError();

            return Ok();
        }

        [Route("Delete/{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var service = GetEventService();

            if (service.DeleteEvent(id))
                return Ok();

            return InternalServerError();
        }

        private EventService GetEventService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new EventService(userID);
            return service;
        }
    }
}
