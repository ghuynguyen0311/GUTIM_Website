using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GYM_MANAGER_PROJECTFSOFT.Models;
using PagedList;
using System.Data.Entity;


namespace GYM_MANAGER_PROJECTFSOFT.Controllers
{
    public class ServiceManagerController : Controller
    {
        GYM_ManangerEntities db = new GYM_ManangerEntities();
        // GET: ServiceManager
        public ActionResult ServiceTable(string SearchString, int? page)
        {
            var pageNum = page ?? 1;
            var pageSize = 3;
            var lstDichVu = db.DICHVUs.Include(x => x.CLUB);
            if (!String.IsNullOrEmpty(SearchString))
            {
                lstDichVu = lstDichVu.Where(s => s.TenDichVu.Contains(SearchString));
            }
            return View(lstDichVu.OrderBy(x => x.MaDichVu).ToPagedList(pageNum, pageSize));
        }

        [HttpGet]
        public ActionResult ServiceEdit(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            DICHVU dICHVU = db.DICHVUs.Find(id);
            if (dICHVU == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaClub = new SelectList(db.CLUBs, "MaClub", "TenClub", dICHVU.MaClub);
            return View(dICHVU);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ServiceEdit([Bind(Include = "MaDichVu,TenDichVu,MaClub,HinhAnh1,HinhAnh2,HinhAnh3")] DICHVU dICHVU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dICHVU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ServiceTable");
            }
            ViewBag.MaClub = new SelectList(db.CLUBs, "MaClub", "TenClub", dICHVU.MaClub);
            return View(dICHVU);
        }

        public ActionResult ServiceDelete(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            DICHVU dICHVU = db.DICHVUs.Find(id);
            if (dICHVU == null)
            {
                return HttpNotFound();
            }
            return View(dICHVU);
        }

        // POST: Admin/DICHVUs/Delete/5
        [HttpPost, ActionName("ServiceDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult ServiceDeleteConfirmed(int id)
        {
            DICHVU dICHVU = db.DICHVUs.Find(id);
            db.DICHVUs.Remove(dICHVU);
            db.SaveChanges();
            return RedirectToAction("ServiceTable");
        }

        [HttpGet]
        public ActionResult CreateService()
        {
            ViewBag.MaClub = new SelectList(db.CLUBs, "MaClub", "TenClub");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateService([Bind(Include = "MaDichVu,TenDichVu,MaClub,HinhAnh1,HinhAnh2,HinhAnh3")] DICHVU dICHVU)
        {
            if (ModelState.IsValid)
            {
                db.DICHVUs.Add(dICHVU);
                db.SaveChanges();
                return RedirectToAction("ServiceTable");
            }

            ViewBag.MaClub = new SelectList(db.CLUBs, "MaClub", "TenClub", dICHVU.MaClub);
            return View(dICHVU);
        }
    }
}