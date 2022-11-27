using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GYM_MANAGER_PROJECTFSOFT.Models;
using PagedList;

namespace GYM_MANAGER_PROJECTFSOFT.Controllers
{
    public class ServicesController : Controller
    {
        GYM_ManangerEntities db = new GYM_ManangerEntities(); 
        // GET: Services

        public ActionResult ServiceDetail(int? MaDichVu)
        {
            var phong = db.Phongs.FirstOrDefault(x => x.MaDichVu == MaDichVu);

            int maphong = phong.MaPhong;

            var thietbi = db.THIETBIs.Where(x => x.MaPhong == maphong);

            ViewBag.ThietBi = thietbi;

            if (MaDichVu == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var goitap = db.GOITAPs.Where(x => x.MaDichVu == MaDichVu);
            return View(goitap);
        }
    }
}