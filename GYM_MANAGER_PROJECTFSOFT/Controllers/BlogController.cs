using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GYM_MANAGER_PROJECTFSOFT.Models;

namespace GYM_MANAGER_PROJECTFSOFT.Controllers
{
    public class BlogController : Controller
    {
        GYM_ManangerEntities db = new GYM_ManangerEntities();
        // GET: Blog
        public ActionResult BlogList()
        {
            var lstblog = db.BLOGs;
            return View(lstblog);
        }

        public ActionResult BlogDetail(int? MaBlog)
        {
            var blogDetail = db.BLOGs.SingleOrDefault(x => x.MaBlog == MaBlog);

            return View(blogDetail);
        }




    }
}