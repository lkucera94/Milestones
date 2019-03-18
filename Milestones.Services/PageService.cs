using Milestones.Data;
using Milestones.Models.PageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milestones.Services
{

    public class PageService
    {
        private readonly Guid _userId;

        public PageService()
        {
        }

        public PageService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreatePage(PageCreate model)
        {
            var entity =
                new Page()
                {
                    PageName = model.PageName,
                    OwnerID = _userId
                };


            using (var ctx = new ApplicationDbContext())
            {
                ctx.Pages.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public bool JoinPage(int pageId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var user =
                    ctx.Users.Single(e => e.Id == _userId.ToString());

                user.PageId = pageId;

                return ctx.SaveChanges() == 1;

            }
        }
    }
}
