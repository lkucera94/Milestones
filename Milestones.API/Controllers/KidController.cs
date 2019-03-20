using Microsoft.AspNet.Identity;
using Milestones.Models.KidModels;
using Milestones.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Milestones.API.Controllers
{
    
        [RoutePrefix("api/Kid")]
        public class KidController : ApiController
        {
            [Route("AllKids")]
            public IHttpActionResult GetAll()
            {
                var service = GetEventService();
                var kids = service.GetKidsByUserID();
                return Ok(kids);
            }

            [Route("ByKid/{id:int}")]
            public IHttpActionResult GetByKidID(int id)
            {
                var service = GetEventService();
                var events = service.GetKidByKidID(id);
                return Ok(events);
            }

            [Route("Create")]
            public IHttpActionResult Post(KidCreate create)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var service = GetEventService();

                if (!service.CreateKid(create))
                    return InternalServerError();

                return Ok();
            }

            [Route("Edit")]
            public IHttpActionResult Put(KidEdit edit)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var service = GetEventService();

                if (!service.EditKid(edit))
                    return InternalServerError();

                return Ok();
            }

            [Route("Delete/{id:int}")]
            public IHttpActionResult DeleteKid(int id)
            {
                var service = GetEventService();

                if (service.DeleteKid(id))
                    return Ok();

                return InternalServerError();
            }

            private KidService GetEventService()
            {
                var userID = Guid.Parse(User.Identity.GetUserId());
                var service = new KidService(userID);
                return service;
            }
        }
    }


