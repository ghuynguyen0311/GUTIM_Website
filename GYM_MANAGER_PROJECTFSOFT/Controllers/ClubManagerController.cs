using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GYM_MANAGER_PROJECTFSOFT.Models;

namespace GYM_MANAGER_PROJECTFSOFT.Controllers
{
    public class ClubManagerController : Controller
    {
        // GET: ClubManager
        GYM_ManangerEntities db = new GYM_ManangerEntities();

        public ActionResult ClubTables()
        {
            var lstClub = from c in db.CLUBs select c;
            return View(lstClub);
        }

        public ActionResult CreateClub()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateClub([Bind(Include = "MaClub,TenClub,DiaChi,SoDienThoai,Gmail,HinhAnh1,HinhAnh2,HinhAnh3")] CLUB cLUB/*, HttpPostedFileBase HinhAnh1, HttpPostedFileBase HinhAnh2, HttpPostedFileBase HinhAnh3*/)
        {
            if (ModelState.IsValid)
            {
                db.CLUBs.Add(cLUB);
                db.SaveChanges();
                return RedirectToAction("ClubTables");
            }

            return View(cLUB);
        }

        public ActionResult EditClub(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            CLUB cLUB = db.CLUBs.Find(id);
            if (cLUB == null)
            {
                return HttpNotFound();
            }
            return View(cLUB);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditClub([Bind(Include = "MaClub,TenClub,DiaChi,SoDienThoai,Gmail,HinhAnh1,HinhAnh2,HinhAnh3")] CLUB cLUB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cLUB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ClubTables");
            }
            return View(cLUB);
        }

        // GET: Admin/CLUBs/Delete/5
        public ActionResult DeleteClub(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            CLUB cLUB = db.CLUBs.Find(id);
            if (cLUB == null)
            {
                return HttpNotFound();
            }
            return View(cLUB);
        }
        public ActionResult DeleteConfirmed(int id)
        {
            CLUB cLUB = db.CLUBs.Find(id);
            db.CLUBs.Remove(cLUB);
            db.SaveChanges();
            return RedirectToAction("ClubTables");
        }

    }
}