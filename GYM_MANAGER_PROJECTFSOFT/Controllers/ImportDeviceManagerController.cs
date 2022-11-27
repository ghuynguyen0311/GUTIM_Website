using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GYM_MANAGER_PROJECTFSOFT.Models;
using PagedList;
using System.Data.Entity;
using System.Net;
using System.IO;

namespace GYM_MANAGER_PROJECTFSOFT.Controllers
{
    public class ImportDeviceManagerController : Controller
    {
        GYM_ManangerEntities db = new GYM_ManangerEntities();
        // GET: ImportDeviceManager
        public ActionResult ImportDeviceTable(string searchString, int? page)
        {
            var pageNum = page ?? 1;
            var pageSize = 10;
            var nhapthietbis = from c in db.NHAPTHIETBIs select c;
            if (!String.IsNullOrEmpty(searchString))
            {
                nhapthietbis = nhapthietbis.Where(x => x.TenThietBi.Equals(searchString));
            }
            return View(nhapthietbis.OrderBy(x => x.MaYeuCauNhap).ToPagedList(pageNum, pageSize));
        }

        public ActionResult CreateImportDevice()
        {
            ViewBag.MaLoaiThietBi = new SelectList(db.LOAITHIETBIs, "MaLoaiThietBi", "TenLoaiThietBi");
            ViewBag.MaTaiKhoan = new SelectList(db.TAIKHOANs.Where(x => x.Role == true), "MaTaiKhoan", "TaiKhoan1");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateImportDevice([Bind(Include = "MaYeuCauNhap,TenThietBi,SoLuong,HinhThietBi,NgayYeuCau,MaTaiKhoan,MoTa,MaLoaiThietBi")] NHAPTHIETBI nHAPTHIETBI)
        {
            if (ModelState.IsValid)
            {
                db.NHAPTHIETBIs.Add(nHAPTHIETBI);
                db.SaveChanges();
                NHAPTHIETBI varCheckTrangThaiThietbi = db.NHAPTHIETBIs.SingleOrDefault(x => x.MaYeuCauNhap == nHAPTHIETBI.MaYeuCauNhap);
                varCheckTrangThaiThietbi.TrangThaiNhapThietBi = false;
                db.SaveChanges();
                return RedirectToAction("ImportDeviceTable");
            }

            ViewBag.MaLoaiThietBi = new SelectList(db.LOAITHIETBIs, "MaLoaiThietBi", "TenLoaiThietBi", nHAPTHIETBI.MaLoaiThietBi);
            ViewBag.MaTaiKhoan = new SelectList(db.TAIKHOANs, "MaTaiKhoan", "TaiKhoan1", nHAPTHIETBI.MaTaiKhoan);
            return View(nHAPTHIETBI);
        }

        public ActionResult EditImportDevice(int? id)
        {

            NHAPTHIETBI nHAPTHIETBI = db.NHAPTHIETBIs.Find(id);
            if (nHAPTHIETBI == null)
            {
                return HttpNotFound();
            }
            if (nHAPTHIETBI.TrangThaiNhapThietBi == true)
            {
                KHOTHIETBI kHOTHIETBI = new KHOTHIETBI();
                kHOTHIETBI.MaYeuCauNhap = nHAPTHIETBI.MaYeuCauNhap;
                db.KHOTHIETBIs.Add(kHOTHIETBI);
                db.SaveChanges();
            }
            ViewBag.MaLoaiThietBi = new SelectList(db.LOAITHIETBIs, "MaLoaiThietBi", "TenLoaiThietBi", nHAPTHIETBI.MaLoaiThietBi);
            ViewBag.MaTaiKhoan = new SelectList(db.TAIKHOANs, "MaTaiKhoan", "TaiKhoan1", nHAPTHIETBI.MaTaiKhoan);
            return View(nHAPTHIETBI);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditImportDevice([Bind(Include = "MaYeuCauNhap,TenThietBi,SoLuong,HinhThietBi,NgayYeuCau,MaTaiKhoan,MoTa,MaLoaiThietBi,TrangThaiNhapThietBi")] NHAPTHIETBI nHAPTHIETBI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nHAPTHIETBI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ImportDeviceTable");
            }
            ViewBag.MaLoaiThietBi = new SelectList(db.LOAITHIETBIs, "MaLoaiThietBi", "TenLoaiThietBi", nHAPTHIETBI.MaLoaiThietBi);
            ViewBag.MaTaiKhoan = new SelectList(db.TAIKHOANs, "MaTaiKhoan", "TaiKhoan1", nHAPTHIETBI.MaTaiKhoan);
            return View(nHAPTHIETBI);
        }

        public ActionResult ApproveImportDevice(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            NHAPTHIETBI nhapthietbi = db.NHAPTHIETBIs.SingleOrDefault(x => x.MaYeuCauNhap == id);
            nhapthietbi.TrangThaiNhapThietBi = true;
            db.SaveChanges();

            KHOTHIETBI khothietbi = new KHOTHIETBI();
            khothietbi.MaYeuCauNhap = nhapthietbi.MaYeuCauNhap;
            db.KHOTHIETBIs.Add(khothietbi);
            db.SaveChanges();
            var setdefault = db.KHOTHIETBIs.SingleOrDefault(x => x.MaYeuCauNhap == nhapthietbi.MaYeuCauNhap);
            khothietbi.TrangThaiTonKho = false;
            db.SaveChanges();

            return RedirectToAction("ImportDeviceTable");
        }
    }
}