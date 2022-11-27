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
    public class PackageAndCouponManagerController : Controller
    {
        GYM_ManangerEntities db = new GYM_ManangerEntities();

        public List<PackageDetail> DisplayPackageDetail()
        {
            List<PackageDetail> list = new List<PackageDetail>();
            var goitap = db.GOITAPs;
            foreach(var item in goitap)
            {
                var dichvu = db.DICHVUs.SingleOrDefault(x => x.MaDichVu == item.MaDichVu);
                var club = db.CLUBs.SingleOrDefault(x => x.MaClub == dichvu.MaClub);
                var packageDetail = new PackageDetail
                {
                    MaGoiTap = item.MaGoiTap,
                    TenGoiTap = item.TenGoiTap,
                    GiaGoiTap = item.GiaGoiTap,
                    TenClub = club.TenClub,
                    TenDichVu = dichvu.TenDichVu,
                    ThoiHanGoiTap = item.ThoiHanGoiTap,
                };
                list.Add(packageDetail);
            }        
            return list;
        }
        // GET: PackageAndCouponManager
        public ActionResult PackageTable(int? page)
        {
            var pageNum = page ?? 1;
            var pageSize = 10;
            List<PackageDetail> list = DisplayPackageDetail();
            return View(list.OrderBy(x => x.MaGoiTap).ToPagedList(pageNum, pageSize));
        }

        [HttpGet]
        public ActionResult EditPackage(int? id)
        {
            int magoitap = (int)id;
            var goitap = db.GOITAPs.SingleOrDefault(x => x.MaGoiTap == id);
            var dichvu = db.DICHVUs.SingleOrDefault(x => x.MaDichVu == goitap.MaDichVu);
            var club = db.CLUBs.SingleOrDefault(x => x.MaClub == dichvu.MaClub);
            var packageDetail = new PackageDetail
            {
                MaGoiTap = goitap.MaGoiTap,
                TenGoiTap = goitap.TenGoiTap,
                GiaGoiTap = goitap.GiaGoiTap,
                TenClub = club.TenClub,
                TenDichVu = dichvu.TenDichVu,
                ThoiHanGoiTap = goitap.ThoiHanGoiTap,
            };
            return View(packageDetail);
        }

        [HttpPost]
        public ActionResult EditPackage(PackageDetail packagedetail)
        {
            var goitap = db.GOITAPs.SingleOrDefault(x => x.MaGoiTap == packagedetail.MaGoiTap);
            goitap.ThoiHanGoiTap = packagedetail.ThoiHanGoiTap;
            goitap.GiaGoiTap = packagedetail.GiaGoiTap;
            db.SaveChanges();
            return View(packagedetail);
        }

        public ActionResult CouponList(int? page)
        {
            var pageNum = page ?? 1;
            var pageSize = 10;
            var lstcoupon = db.GIAMGIAs;
            return View(lstcoupon.OrderBy(x => x.MaGiamGia).ToPagedList(pageNum, pageSize));
        }

        [HttpGet]
        public ActionResult EditCoupon(int? id)
        {
            int magiamgia = (int)id;
            var coupon = db.GIAMGIAs.SingleOrDefault(x => x.MaGiamGia == magiamgia);
            return View(coupon);
        }

        [HttpPost]
        public ActionResult EditCoupon(GIAMGIA coupon)
        {
            int magiamgia = (int)coupon.MaGiamGia;
            var editcoupon = db.GIAMGIAs.SingleOrDefault(x => x.MaGiamGia == magiamgia);
            editcoupon.SoTienGiamGia = coupon.SoTienGiamGia;
            editcoupon.Soluong = coupon.Soluong;
            db.SaveChanges();
            return View(coupon);
        }
    }
}