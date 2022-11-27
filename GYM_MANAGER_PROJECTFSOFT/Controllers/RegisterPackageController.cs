using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GYM_MANAGER_PROJECTFSOFT.Models;

namespace GYM_MANAGER_PROJECTFSOFT.Controllers
{
    public class RegisterPackageController : Controller
    {
        GYM_ManangerEntities db = new GYM_ManangerEntities();
        [HttpGet]
        public ActionResult RegisterPackageForm(int MaTaiKhoan, int MaDichVu, int MaGoiTap, string strURL)
        {
            Profile profileUser = db.Profiles.SingleOrDefault(x => x.MaTaiKhoan == MaTaiKhoan);
            ViewBag.Profile = profileUser;
            if (profileUser.MaThanhVien == null)
            {
                return Redirect(strURL);
            }
            TAIKHOAN taikhoanUser = db.TAIKHOANs.SingleOrDefault(x => x.MaTaiKhoan == MaTaiKhoan);
            GOITAP goitap = db.GOITAPs.SingleOrDefault(x => x.MaDichVu == MaDichVu && x.MaGoiTap == MaGoiTap);
            DICHVU dichvu = db.DICHVUs.SingleOrDefault(x => x.MaDichVu == MaDichVu);
            CLUB club = db.CLUBs.SingleOrDefault(x => x.MaClub == dichvu.MaClub);
            DateTime dateTime = DateTime.Now;

            //Check trạng thái đã có gói tập hay chưa 
            //Begin Code
            THANHVIEN thanhvien = db.THANHVIENs.SingleOrDefault(x => x.MaThanhVien == profileUser.MaThanhVien);
            THETHANHVIEN thethanhvien = db.THETHANHVIENs.SingleOrDefault(x => x.MaTheThanhVien == thanhvien.MaTheThanhVien);
            ViewBag.TheThanhVien = thethanhvien;
            //End Code

            String coupon = "";

            var goiTapAndProfileDetail = new GoiTapAndProfileDetail
            {
                //Profile Detail
                MaTaiKhoan = taikhoanUser.MaTaiKhoan,
                MaProfile = profileUser.MaProfile,
                HoTen = profileUser.HoTen,
                Email = taikhoanUser.Email,
                SoDienThoai = taikhoanUser.SoDienThoai,
                DiaChi = profileUser.DiaChi,
                //Package Detail
                MaDichVu = dichvu.MaDichVu,
                MaGoiTap = goitap.MaGoiTap,
                TenDichVu = dichvu.TenDichVu,
                TenGoiTap = goitap.TenGoiTap,
                DiaChiDichVu = club.DiaChi,
                ThoiHanGoiTap = goitap.ThoiHanGoiTap,
                GiaGoiTap = goitap.GiaGoiTap,
                NgayDangKy = dateTime,
                TotalAmount = goitap.GiaGoiTap,
                Coupon = coupon,
            };
            return View(goiTapAndProfileDetail);
        }

        [HttpPost]
        public ActionResult registerpackageform(GoiTapAndProfileDetail dangkygoitapdetail)
        {
            Profile profileuser = db.Profiles.SingleOrDefault(x => x.MaTaiKhoan == dangkygoitapdetail.MaTaiKhoan);
            ViewBag.profile = profileuser;

            TAIKHOAN taikhoanuser = db.TAIKHOANs.SingleOrDefault(x => x.MaTaiKhoan == dangkygoitapdetail.MaTaiKhoan);
            GOITAP goitap = db.GOITAPs.SingleOrDefault(x =>x.MaDichVu == dangkygoitapdetail.MaDichVu && x.MaGoiTap == dangkygoitapdetail.MaGoiTap);
            DICHVU dichvu = db.DICHVUs.SingleOrDefault(x => x.MaDichVu == dangkygoitapdetail.MaDichVu);
            CLUB club = db.CLUBs.SingleOrDefault(x => x.MaClub == dichvu.MaClub);
            DateTime datetime = DateTime.Now;

            int thoihangoitap = (int)dangkygoitapdetail.ThoiHanGoiTap;
            DateTime ngaydangky = (DateTime)dangkygoitapdetail.NgayDangKy;
            DateTime ngayhethan = ngaydangky.AddDays(thoihangoitap);

            //Check trạng thái đã có gói tập hay chưa 
            //Begin Code
            THANHVIEN thanhvien = db.THANHVIENs.SingleOrDefault(x => x.MaThanhVien == profileuser.MaThanhVien);
            THETHANHVIEN thethanhvien = db.THETHANHVIENs.SingleOrDefault(x => x.MaTheThanhVien == thanhvien.MaTheThanhVien);
            ViewBag.TheThanhVien = thethanhvien;
            //End Code

            if (dangkygoitapdetail.Coupon == null)
            {
                var goitapandprofiledetail = new GoiTapAndProfileDetail
                {
                    // Profile detail
                    MaTaiKhoan = dangkygoitapdetail.MaTaiKhoan,
                    MaProfile = dangkygoitapdetail.MaProfile,
                    HoTen = dangkygoitapdetail.HoTen,
                    Email = dangkygoitapdetail.Email,
                    SoDienThoai = dangkygoitapdetail.SoDienThoai,
                    DiaChi = dangkygoitapdetail.DiaChi,
                    //package detail
                    MaDichVu = dangkygoitapdetail.MaDichVu,
                    MaGoiTap = dangkygoitapdetail.MaGoiTap,
                    TenDichVu = dangkygoitapdetail.TenDichVu,
                    TenGoiTap = dangkygoitapdetail.TenGoiTap,
                    DiaChiDichVu = dangkygoitapdetail.DiaChiDichVu,
                    ThoiHanGoiTap = dangkygoitapdetail.ThoiHanGoiTap,
                    GiaGoiTap = dangkygoitapdetail.GiaGoiTap,
                    NgayDangKy = dangkygoitapdetail.NgayDangKy,
                    TotalAmount = dangkygoitapdetail.TotalAmount,
                    Coupon = dangkygoitapdetail.Coupon,
                };
                return RedirectToAction("ConfirmPackage", goitapandprofiledetail);
            }
            else
            {
                GIAMGIA coupon = db.GIAMGIAs.SingleOrDefault(x => x.CodeGiamGia.Equals(dangkygoitapdetail.Coupon) && x.MaGoiTap == dangkygoitapdetail.MaGoiTap);
                if (coupon != null)
                {
                    var totalamount = goitap.GiaGoiTap - coupon.SoTienGiamGia;
                    var goitapandprofiledetail = new GoiTapAndProfileDetail
                    {
                        // Profile detail
                        MaTaiKhoan = dangkygoitapdetail.MaTaiKhoan,
                        MaProfile = dangkygoitapdetail.MaProfile,
                        HoTen = dangkygoitapdetail.HoTen,
                        Email = dangkygoitapdetail.Email,
                        SoDienThoai = dangkygoitapdetail.SoDienThoai,
                        DiaChi = dangkygoitapdetail.DiaChi,
                        //package detail
                        MaDichVu = dangkygoitapdetail.MaDichVu,
                        MaGoiTap = dangkygoitapdetail.MaGoiTap,
                        TenDichVu = dangkygoitapdetail.TenDichVu,
                        TenGoiTap = dangkygoitapdetail.TenGoiTap,
                        DiaChiDichVu = dangkygoitapdetail.DiaChiDichVu,
                        ThoiHanGoiTap = dangkygoitapdetail.ThoiHanGoiTap,
                        GiaGoiTap = dangkygoitapdetail.GiaGoiTap,
                        NgayDangKy = dangkygoitapdetail.NgayDangKy,
                        TotalAmount = totalamount,
                        Coupon = dangkygoitapdetail.Coupon,
                    };                 
                    return RedirectToAction("ConfirmPackage", goitapandprofiledetail);
                }
                else
                {
                    var goitapandprofiledetail = new GoiTapAndProfileDetail
                    {
                        // Profile detail
                        MaTaiKhoan = dangkygoitapdetail.MaTaiKhoan,
                        MaProfile = dangkygoitapdetail.MaProfile,
                        HoTen = dangkygoitapdetail.HoTen,
                        Email = dangkygoitapdetail.Email,
                        SoDienThoai = dangkygoitapdetail.SoDienThoai,
                        DiaChi = dangkygoitapdetail.DiaChi,
                        //package detail
                        MaDichVu = dangkygoitapdetail.MaDichVu,
                        MaGoiTap = dangkygoitapdetail.MaGoiTap,
                        TenDichVu = dangkygoitapdetail.TenDichVu,
                        TenGoiTap = dangkygoitapdetail.TenGoiTap,
                        DiaChiDichVu = dangkygoitapdetail.DiaChiDichVu,
                        ThoiHanGoiTap = dangkygoitapdetail.ThoiHanGoiTap,
                        GiaGoiTap = dangkygoitapdetail.GiaGoiTap,
                        NgayDangKy = dangkygoitapdetail.NgayDangKy,
                        TotalAmount = dangkygoitapdetail.TotalAmount,
                        Coupon = dangkygoitapdetail.Coupon,
                    };
                    return RedirectToAction("ConfirmPackage", goitapandprofiledetail);
                }
            }      
        }

        [HttpGet]
        public ActionResult ConfirmPackage(GoiTapAndProfileDetail confirmPackage)
        {
            Profile profileuser = db.Profiles.SingleOrDefault(x => x.MaTaiKhoan == confirmPackage.MaTaiKhoan);
            ViewBag.profile = profileuser;

            THANHVIEN thanhvien = db.THANHVIENs.SingleOrDefault(x => x.MaThanhVien == profileuser.MaThanhVien);
            THETHANHVIEN thethanhvien = db.THETHANHVIENs.SingleOrDefault(x => x.MaTheThanhVien == thanhvien.MaTheThanhVien);
            //ViewBag.TheThanhVien = thethanhvien;

            ViewBag.TheThanhVien = thethanhvien;

            if (confirmPackage == null)
            {
                ViewBag.Success = "Please confirm your decision.";
            } else
            {
                GIAMGIA coupon = db.GIAMGIAs.SingleOrDefault(x => x.CodeGiamGia.Equals(confirmPackage.Coupon) && x.MaGoiTap == confirmPackage.MaGoiTap);
                if (coupon != null)
                {
                    // giảm số lượng coupon sau khi add
                    //Begin
                    int couponafteradd = Convert.ToInt32(coupon.Soluong);
                    --couponafteradd;
                    string couponEdit = Convert.ToString(couponafteradd);
                    coupon.Soluong = couponEdit;
                    db.SaveChanges();
                    //End
                    ViewBag.Success = "Add Coupon Success! Please confirm your decision.";
                } else
                {
                    ViewBag.Success = "Add Coupon Fail! Please confirm your decision.";
                }
            }

            return View(confirmPackage);
        }

        [HttpPost]
        public ActionResult ConfirmPackage(GoiTapAndProfileDetail confirmPackage, FormCollection form)
        {
            Profile profileuser = db.Profiles.SingleOrDefault(x => x.MaTaiKhoan == confirmPackage.MaTaiKhoan);
            ViewBag.profile = profileuser;

            THANHVIEN thanhvien = db.THANHVIENs.SingleOrDefault(x => x.MaThanhVien == profileuser.MaThanhVien);
            THETHANHVIEN thethanhvien = db.THETHANHVIENs.SingleOrDefault(x => x.MaTheThanhVien == thanhvien.MaTheThanhVien);

            //int thoihangoitap = (int)confirmPackage.ThoiHanGoiTap;
            //DateTime ngaydangky = (DateTime)confirmPackage.NgayDangKy;
            //DateTime ngayhethan = ngaydangky.AddDays(thoihangoitap);

            DANGKYGOITAP dangkygoitap = new DANGKYGOITAP();
            dangkygoitap.MaGoiTap = confirmPackage.MaGoiTap;
            dangkygoitap.NgayDangKi = confirmPackage.NgayDangKy;
            dangkygoitap.GiaDangKi = confirmPackage.TotalAmount;
            db.DANGKYGOITAPs.Add(dangkygoitap);
            db.SaveChanges();

            //thethanhvien.NgayDangKy = confirmPackage.NgayDangKy;
            //thethanhvien.NgayHetHan = ngayhethan;
            //thethanhvien.MaDangKyGoiTap = dangkygoitap.MaDangKyGoiTap;

            // Phần edit begin 
            thethanhvien.MaDangKyGoiTap = dangkygoitap.MaDangKyGoiTap;
            thethanhvien.TrangThai = false;

            ViewBag.TheThanhVien = thethanhvien;

            db.SaveChanges();

            //Session["TheThanhVien"] = thethanhvien;
            //thanhvien.NgayDangKi = confirmPackage.NgayDangKy;
            //db.SaveChanges();

            ViewBag.Success = "Thanks to join with us! Your member card is waiting for validation.";

            return View(confirmPackage);
        }
    }
}