using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GYM_MANAGER_PROJECTFSOFT.Models;
using System.IO;

namespace GYM_MANAGER_PROJECTFSOFT.Controllers
{
    public class UserController : Controller
    {
        GYM_ManangerEntities db = new GYM_ManangerEntities();

        public ProfileDetail DisplayUserDetail(int MaTaiKhoan)
        {
            Profile profileUser = db.Profiles.SingleOrDefault(x => x.MaTaiKhoan == MaTaiKhoan);
            TAIKHOAN taikhoanUser = db.TAIKHOANs.SingleOrDefault(x => x.MaTaiKhoan == MaTaiKhoan);
            if(profileUser != null)
            {
                var profiledetail = new ProfileDetail
                {
                    MaTaiKhoan = taikhoanUser.MaTaiKhoan,
                    TaiKhoan = taikhoanUser.TaiKhoan1,
                    MatKhau = taikhoanUser.MatKhau,
                    Email = taikhoanUser.Email,
                    SoDienThoai = taikhoanUser.SoDienThoai,
                    HoTen = profileUser.HoTen,
                    DiaChi = profileUser.DiaChi,
                    NgaySinh = profileUser.NgaySinh,
                    Avatar = profileUser.Avatar,
                };
                return profiledetail;
            } else
            {
                var profiledetail = new ProfileDetail
                {
                    MaTaiKhoan = taikhoanUser.MaTaiKhoan,
                    TaiKhoan = taikhoanUser.TaiKhoan1,
                    MatKhau = taikhoanUser.MatKhau,
                    Email = taikhoanUser.Email,
                    SoDienThoai = taikhoanUser.SoDienThoai,
                };
                return profiledetail;
            }
            return null;
        }
        // GET: User
        [HttpGet]
        public ActionResult ProfileUser(int? MaTaiKhoan)
        {
            if (MaTaiKhoan == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            int mataikhoan = (int)MaTaiKhoan;
            var profiledetail = DisplayUserDetail(mataikhoan);

            Model1GroupTheThanhVien thethanhviendetail = DisplayMemberCard(mataikhoan);
            if(thethanhviendetail == null)
            {
                return View(profiledetail);
            } else
            {
                Session["MemberCard"] = thethanhviendetail;
                ViewBag.TheThanhVienDetail = thethanhviendetail;
                return View(profiledetail);
            }
        }

        [HttpPost]
        public ActionResult ProfileUser(ProfileDetail profileDetail, FormCollection form)
        {
            if (profileDetail == null)
            {
                Response.StatusCode = 404;
                return null;
            }


            TAIKHOAN taikhoan = db.TAIKHOANs.SingleOrDefault(x => x.MaTaiKhoan == profileDetail.MaTaiKhoan);
            taikhoan.TaiKhoan1 = profileDetail.TaiKhoan;
            taikhoan.Email = profileDetail.Email;
            taikhoan.SoDienThoai = profileDetail.SoDienThoai;
            db.SaveChanges();

            

            Profile profile = db.Profiles.SingleOrDefault(x => x.MaTaiKhoan == profileDetail.MaTaiKhoan);
            if(profile != null)
            {
                profile.HoTen = profileDetail.HoTen;
                profile.DiaChi = profileDetail.DiaChi;
                profile.NgaySinh = profileDetail.NgaySinh;
                db.SaveChanges();

                profileDetail.Avatar = profile.Avatar;

                if (profileDetail.MatKhau == taikhoan.MatKhau)
                {
                    if (form["txtRepassword"] == null || form["txtRepassword"] == "")
                    {
                        ViewBag.MessageSuccess = "Update Success";
                        return View(profileDetail);
                    }
                    else
                    {
                        ViewBag.MessageError = "Do not input comfirm password if you do not change password";
                        return View(profileDetail);
                    }
                }
                else
                {
                    if (form["txtRepassword"] == null || form["txtRepassword"] == "" || profileDetail.MatKhau != form["txtRepassword"])
                    {
                        ViewBag.MessageError = "Password and Confirm Password are not match";
                        return View(profileDetail);
                    }
                    else
                    {
                        taikhoan.MatKhau = profileDetail.MatKhau;
                        db.SaveChanges();
                        ViewBag.MessageSuccess = "Update Success";
                        return View(profileDetail);
                    }
                }
            }
            else
            {
                Profile newProfile = new Profile();
                newProfile.DiaChi = profileDetail.DiaChi;
                newProfile.HoTen = profileDetail.HoTen;
                newProfile.NgaySinh = profileDetail.NgaySinh;
                newProfile.MaTaiKhoan = profileDetail.MaTaiKhoan;
                db.Profiles.Add(newProfile);
                db.SaveChanges();
                return View(profileDetail);
            }
           
            Model1GroupTheThanhVien thethanhviendetail = DisplayMemberCard(profileDetail.MaTaiKhoan);
            if (thethanhviendetail == null)
            {
                return View(profileDetail);
            }
            else
            {
                Session["MemberCard"] = thethanhviendetail;
                ViewBag.TheThanhVienDetail = thethanhviendetail;
                return View(profileDetail);
            }
        }

        [HttpPost]
        public ActionResult UploadAvatar(HttpPostedFileBase file, int MaTaiKhoan)
        {
            int mataikhoan = (int)MaTaiKhoan;
            try
            {
                if (file.ContentLength > 0)
                {
                    string _filename = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/ImagesGym"), _filename);
                    file.SaveAs(_path);
                    var profileAvatar = db.Profiles.SingleOrDefault(x => x.MaTaiKhoan == mataikhoan);
                    if (profileAvatar != null)
                    {
                        string fileAvatar = (string)file.FileName;
                        profileAvatar.Avatar = fileAvatar;
                        db.SaveChanges();
                    }
                    else
                    {
                        string fileAvatar = (string)file.FileName;
                        Profile profile = new Profile();
                        profile.Avatar = fileAvatar;
                        profile.MaTaiKhoan = mataikhoan;
                        db.Profiles.Add(profile);
                        db.SaveChanges();
                    }
                }
                ViewBag.Message = "Success!!";
                return RedirectToAction("ProfileUser", new {MaTaiKhoan = mataikhoan});
            }
            catch
            {
                ViewBag.Message = "Fail!";
                return RedirectToAction("ProfileUser", new {MaTaiKhoan = mataikhoan});
            }
        }


        public static string RandomChar()
        {
            string randomStr = "";
            try
            {

                string[] myIntArray = new string[5];
                int x;
                Random autoRand = new Random();
                for (x = 0; x < 5; x++)
                {
                    myIntArray[x] = Convert.ToChar(Convert.ToInt32(autoRand.Next(65, 87))).ToString();
                    randomStr += (myIntArray[x].ToString());
                }
            }
            catch (Exception ex)
            {
                randomStr = "error";
            }
            return randomStr;
        }

        [HttpGet]
        public ActionResult CreateMemberCard(int? MaTaiKhoan)
        {
            if(MaTaiKhoan == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            
            int mataikhoan = (int)MaTaiKhoan;
            LOAITHETHANHVIEN loaithetv = db.LOAITHETHANHVIENs.SingleOrDefault(x => x.MaLoaiTheThanhVien == 1);
            TheThanhVienDetail thethanhvienDetail = new TheThanhVienDetail
            {
                MaLoaiTheThanhVien = 1,
                TenThe = "Thẻ " + loaithetv.TenLoai,
                CodeThe = RandomChar(),
                HinhAnhThe = loaithetv.HinhAnhThe,
                MaTaiKhoan = mataikhoan,  
            };
            return View(thethanhvienDetail);
        }

        [HttpPost]
        public ActionResult CreateMemberCard(TheThanhVienDetail thethanhviendetail)
        {
            if(thethanhviendetail == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            THETHANHVIEN thethanhvien = new THETHANHVIEN();
            thethanhvien.CodeThe = thethanhviendetail.CodeThe;
            thethanhvien.TenThe = thethanhviendetail.TenThe;
            //thethanhvien.NgayDangKy = thethanhviendetail.NgayDangKy;
            thethanhvien.MaLoaiTheThanhVien = thethanhviendetail.MaLoaiTheThanhVien;
            db.THETHANHVIENs.Add(thethanhvien);
            db.SaveChanges();

            THANHVIEN thanhvien = new THANHVIEN();
            thanhvien.MaTheThanhVien = thethanhvien.MaTheThanhVien;
            db.THANHVIENs.Add(thanhvien);
            db.SaveChanges();

            Profile profile = db.Profiles.SingleOrDefault(x => x.MaTaiKhoan == thethanhviendetail.MaTaiKhoan);

            profile.MaThanhVien = thanhvien.MaThanhVien;
            db.SaveChanges();

            ViewBag.Message = "Create Success!";

            return View(thethanhviendetail);
        }

        public Model1GroupTheThanhVien DisplayMemberCard(int? MaTaiKhoan)
        {
            if (MaTaiKhoan == null)
            {
                return null;
            }

            Profile profile = db.Profiles.SingleOrDefault(x => x.MaTaiKhoan == MaTaiKhoan);
            if (profile == null)
            {
                return null;
            }   
            THANHVIEN thanhvien = db.THANHVIENs.SingleOrDefault(x => x.MaThanhVien == profile.MaThanhVien);
            if (thanhvien != null)
            {
                THETHANHVIEN thethanhvien = db.THETHANHVIENs.SingleOrDefault(x => x.MaTheThanhVien == thanhvien.MaTheThanhVien);
                    LOAITHETHANHVIEN loaithethanhvien = db.LOAITHETHANHVIENs.SingleOrDefault(x => x.MaLoaiTheThanhVien == thethanhvien.MaLoaiTheThanhVien);
                    ProfileGroupThanhVien profileGroupThanhVien = new ProfileGroupThanhVien
                    {
                        HoTen = profile.HoTen,
                        MaTaiKhoan = (int)profile.MaTaiKhoan,
                        MaThanhVien = (int)profile.MaThanhVien,
                        MaTheThanhVien = (int)thanhvien.MaTheThanhVien,
                    };

                    Model1GroupTheThanhVien thethanhviendetail = new Model1GroupTheThanhVien
                    {
                        MaTaiKhoan = (int)profile.MaTaiKhoan,
                        HoTen = profile.HoTen,
                        MaThanhVien = (int)thanhvien.MaThanhVien,
                        MaTheThanhVien = (int)thanhvien.MaTheThanhVien,
                        CodeThe = thethanhvien.CodeThe,
                        NgayDangKy = thethanhvien.NgayDangKy,
                        NgayHetHan = thethanhvien.NgayHetHan,
                        TenLoai = loaithethanhvien.TenLoai,
                        HinhAnhThe = loaithethanhvien.HinhAnhThe,
                    };
                    return thethanhviendetail;
            }
            else
            {
                return null;
            }      
        }
    }
}