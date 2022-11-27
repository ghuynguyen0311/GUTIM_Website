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
    public class DeviceManagerController : Controller
    {
        GYM_ManangerEntities db = new GYM_ManangerEntities();
        // GET: DeviceManager
        public ActionResult DeviceTables(string SearchString, int? page)
        {
            var pageNum = page ?? 1;
            var pageSize = 10;
            var lstThietBi = db.THIETBIs.Where(x => x.TrangThaiThietBi == true);
            if (!String.IsNullOrEmpty(SearchString))
            {
                lstThietBi = lstThietBi.Where(s => s.TenThietBi.Contains(SearchString));
            }
            return View(lstThietBi.OrderBy(x => x.MaThietBi).ToPagedList(pageNum, pageSize));
        }

        public ActionResult EditDevice(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            THIETBI tHIETBI = db.THIETBIs.Find(id);
            if (tHIETBI == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiThietBi = new SelectList(db.LOAITHIETBIs, "MaLoaiThietBi", "TenLoaiThietBi", tHIETBI.MaLoaiThietBi);
            ViewBag.MaPhong = new SelectList(db.Phongs, "MaPhong", "TenPhong", tHIETBI.MaPhong);
            return View(tHIETBI);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDevice([Bind(Include = "MaThietBi,TenThietBi,MaPhong,MaLoaiThietBi,NgayNhapThietBi,TrangThaiThietBi,HinhThietBi")] THIETBI tHIETBI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tHIETBI).State = EntityState.Modified;
                db.SaveChanges();
                if(tHIETBI.TrangThaiThietBi == false)
                {
                    KHOTHIETBI khothietbi = new KHOTHIETBI();
                    khothietbi.MaThietBi = tHIETBI.MaThietBi;
                    khothietbi.TrangThaiTonKho = false;
                    db.KHOTHIETBIs.Add(khothietbi);
                    db.SaveChanges();
                }

                return RedirectToAction("DeviceTables");
            }
            ViewBag.MaLoaiThietBi = new SelectList(db.LOAITHIETBIs, "MaLoaiThietBi", "TenLoaiThietBi", tHIETBI.MaLoaiThietBi);
            ViewBag.MaPhong = new SelectList(db.Phongs, "MaPhong", "TenPhong", tHIETBI.MaPhong);
            return View(tHIETBI);
        }

        public ActionResult DeleteDevice(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            THIETBI tHIETBI = db.THIETBIs.Find(id);
            if (tHIETBI == null)
            {
                return HttpNotFound();
            }
            return View(tHIETBI);
        }

        // POST: Admin/THIETBIs/Delete/5
        [HttpPost, ActionName("DeleteDevice")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDeviceConfirmed(int id)
        {
            THIETBI tHIETBI = db.THIETBIs.Find(id);
            db.THIETBIs.Remove(tHIETBI);
            db.SaveChanges();
            return RedirectToAction("DeviceTables");
        }

        //public ActionResult CreateDevice()
        //{
        //    ViewBag.MaLoaiThietBi = new SelectList(db.LOAITHIETBIs, "MaLoaiThietBi", "TenLoaiThietBi");
        //    ViewBag.MaPhong = new SelectList(db.Phongs, "MaPhong", "TenPhong");
        //    return View();
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreateDevice([Bind(Include = "MaThietBi,TenThietBi,MaPhong,MaLoaiThietBi,NgayNhapThietBi,TrangThaiThietBi,HinhThietBi")] THIETBI tHIETBI)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.THIETBIs.Add(tHIETBI);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.MaLoaiThietBi = new SelectList(db.LOAITHIETBIs, "MaLoaiThietBi", "TenLoaiThietBi", tHIETBI.MaLoaiThietBi);
        //    ViewBag.MaPhong = new SelectList(db.Phongs, "MaPhong", "TenPhong", tHIETBI.MaPhong);
        //    return View(tHIETBI);
        //}
    }
}