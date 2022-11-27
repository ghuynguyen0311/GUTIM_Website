using GYM_MANAGER_PROJECTFSOFT.Models;
using System.Linq;
using System.Web.Mvc;

namespace GYM_MANAGER_PROJECTFSOFT.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        GYM_ManangerEntities db = new GYM_ManangerEntities();

        public ActionResult Index()
        {
            var lstDichVu = db.DICHVUs.Where(x => x.MaClub == 1);
            return View(lstDichVu);
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            string username = form["txtUsername"].ToString();
            string password = form["txtPassword"].ToString();

            TAIKHOAN taikhoan = db.TAIKHOANs.SingleOrDefault(x => x.TaiKhoan1 == username && x.MatKhau == password);
            TAIKHOAN admincaonhat = db.TAIKHOANs.SingleOrDefault(x => x.TaiKhoan1 == "Kit");
            

            if (taikhoan != null)
            {
                Profile profile = db.Profiles.SingleOrDefault(x => x.MaTaiKhoan == taikhoan.MaTaiKhoan);
                Session["TaiKhoan"] = taikhoan;
                Session["Profile"] = profile;
                Session["Role"] = taikhoan.Role;
                return RedirectToAction("Index");
            }

            ViewBag.MessageLogin = "Wrong Your Account Or Password";
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("Index");
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(TAIKHOAN user, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                var check = db.TAIKHOANs.SingleOrDefault(x => x.TaiKhoan1 == user.TaiKhoan1);
                if (check == null)
                {
                    if(user.MatKhau == form["txtPassword"])
                    {
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.TAIKHOANs.Add(user);
                        db.SaveChanges();
                        var setRole = db.TAIKHOANs.SingleOrDefault(x => x.TaiKhoan1 == user.TaiKhoan1 && x.MatKhau == user.MatKhau);
                        setRole.Role = false;
                        db.SaveChanges();
                        ViewBag.Message = "Register success! Please login again";
                        return View(user);
                    } else
                    {
                        db.Configuration.ValidateOnSaveEnabled = false;
                        ViewBag.Message = "Password and Confirm Password do not match";
                        return View(user);
                    }           
                }
                else
                {
                    ViewBag.Message = "Username already exits!Please use another username please.";
                    return View(user);
                }
            }
            return View();
        }

        public ActionResult MenuParital()
        {
            var lstService = db.DICHVUs;
            return PartialView(lstService);
        }

        public ActionResult DeviceMap()
        {
            return View();
        }
    }
}