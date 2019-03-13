using Microsoft.AspNet.Identity;
using Milestones.Models.PageModels;
using Milestones.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Milestones.API.Controllers
{
    public class PageController : ApiController
    {
        public IHttpActionResult Post(PageCreate page)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = new PageService();

            if (!service.CreatePage(page))
                return InternalServerError();

            return Ok();

        }

        public IHttpActionResult Join(int id)
        {
            var service = CreatePageService();
            var join = service.JoinPage(id);

            return Ok();
        }

        private PageService CreatePageService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var toDoService = new PageService(userId);
            return toDoService;
        }

    }
}
