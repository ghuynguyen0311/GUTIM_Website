using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GYM_MANAGER_PROJECTFSOFT.Models;

namespace GYM_MANAGER_PROJECTFSOFT.Controllers
{
    public class BlogManagerController : Controller
    {
        GYM_ManangerEntities db = new GYM_ManangerEntities();
        // GET: BlogManager
        public ActionResult BlogList()
        {
            var lstBlog = db.BLOGs;
            return View(lstBlog);
        }

        [HttpGet]
        public ActionResult AddBlog()
        {

            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddBlog(BLOG model)
        {
            if (model == null)
            {
                Response.StatusCode = 404;
                return null;

            }
            if (ModelState.IsValid)
            {
                db.BLOGs.Add(model);
                db.SaveChanges();
            }

            return RedirectToAction("AddBlog");
        }


        [HttpGet]
        public ActionResult EditBlog(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            BLOG blog = db.BLOGs.SingleOrDefault(x => x.MaBlog == id);

            if (blog == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(blog);
        }

        [HttpPost]
        public ActionResult EditBlog(BLOG blog)
        {
            if (blog == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            BLOG editblog = db.BLOGs.SingleOrDefault(x => x.MaBlog == blog.MaBlog);

            if (editblog != null)
            {
                editblog.TieuDe = blog.TieuDe;
                editblog.NoiDung = blog.NoiDung;
                editblog.HinhAnh = blog.HinhAnh;
                editblog.NgayDang = blog.NgayDang;
                db.SaveChanges();

                ViewBag.Message = "Edit success";
            }
            return RedirectToAction("BlogList");
        }

        public ActionResult DeleteBlog(int? id)
        {
            int mablog = (int)id;
            var Blog = db.BLOGs.SingleOrDefault(x => x.MaBlog == mablog);
            db.BLOGs.Remove(Blog);
            db.SaveChanges();
            return RedirectToAction("BlogList");
        }
    }
}