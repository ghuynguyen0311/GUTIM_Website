using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GYM_MANAGER_PROJECTFSOFT.Models;
using PagedList;
using System.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using GYM_MANAGER_PROJECTFSOFT.Security;

namespace GYM_MANAGER_PROJECTFSOFT.Controllers
{
    public class StatisticalController : Controller
    {
        GYM_ManangerEntities db = new GYM_ManangerEntities();
        // GET: Statistical
        public ActionResult StatisticalIndex()
        {
            ViewBag.PageView = HttpContext.Application["PageView"].ToString();
            ViewBag.RevenueStatistics = RevenueStatistics();
            ViewBag.PendingRequest = PendingRequest();
            ViewBag.CountMember = CountMember();
            ViewBag.TranslateVND = TranslateVND();
            return View();
        }

        public decimal RevenueStatistics()
        {
            decimal revenuestatistics = db.DANGKYGOITAPs.Sum(x => x.GiaDangKi).Value;
            return revenuestatistics;
        }

        public int PendingRequest()
        {
            var pendingrequest = db.THETHANHVIENs.Where(x => x.TrangThai == false);
            int count = pendingrequest.Count();
            return count;
        }

        public double TranslateVND()
        {
            var revenuestatistics = (double)db.DANGKYGOITAPs.Sum(x => x.GiaDangKi).Value;
            double translate = revenuestatistics * 23.4;
            var display = translate.ToString("#,##0.00 VNĐ");
            return translate;
        }

        public int CountMember()
        {
            var countmenber = db.TAIKHOANs;
            var count = countmenber.Count();
            return count;
        }

        //public ActionResult GetReportByPrice(int giadangky)
        //{
        //    var lsDate = db.DANGKYGOITAPs.Where(x => x.GiaDangKi > giadangky);
        //    return Json(lsDate, JsonRequestBehavior.AllowGet);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                    db.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public List<ExportExcelModel> DisplayExportExcelModel()
        {
            List<ExportExcelModel> list = new List<ExportExcelModel>();
            var dangkygoitap = db.DANGKYGOITAPs;
            foreach(var itemDangkygoitap in dangkygoitap)
            {
                THETHANHVIEN thethanhvien = db.THETHANHVIENs.SingleOrDefault(x => x.MaDangKyGoiTap == itemDangkygoitap.MaDangKyGoiTap);
                GOITAP goitap = db.GOITAPs.SingleOrDefault(x => x.MaGoiTap == itemDangkygoitap.MaGoiTap);
                DICHVU dichvu = db.DICHVUs.SingleOrDefault(x => x.MaDichVu == goitap.MaDichVu);
                CLUB club = db.CLUBs.SingleOrDefault(x => x.MaClub == dichvu.MaClub);
                ExportExcelModel exportExcelModel = new ExportExcelModel
                {
                    MaDangKyGoiTap = itemDangkygoitap.MaDangKyGoiTap,
                    TenClub = club.TenClub,
                    TenGoiTap = goitap.TenGoiTap,
                    CodeThe = thethanhvien.CodeThe,
                    NgayDangKi = itemDangkygoitap.NgayDangKi,
                    GiaDangKi = itemDangkygoitap.GiaDangKi,
                }; 
                list.Add(exportExcelModel);
            }
            return list;
        }
        public ActionResult ExportExcelList(int? page)
        {
            var pageNum = page ?? 1;
            var pageSize = 5;
            List<ExportExcelModel> list = DisplayExportExcelModel();
            return View(list.OrderBy(x=>x.MaDangKyGoiTap).ToPagedList(pageNum, pageSize));
        }

        public void ExportToExcel()
        {
            List<ExportExcelModel> list = DisplayExportExcelModel();

            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("DailyIncomeDetail");
            Sheet.Cells["A1"].Value = "Register Code";
            Sheet.Cells["B1"].Value = "Club Name";
            Sheet.Cells["C1"].Value = "Package Name";
            Sheet.Cells["D1"].Value = "Card Code";
            Sheet.Cells["E1"].Value = "Register Date";
            Sheet.Cells["F1"].Value = "Peer";

            int row = 2;
            foreach (var item in list)
            {
                Sheet.Cells[string.Format("A{0}", row)].Value = item.MaDangKyGoiTap;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.TenClub;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.TenGoiTap;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.CodeThe;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.NgayDangKi;
                Sheet.Cells[string.Format("F{0}", row)].Value = item.GiaDangKi;
                row++;
            }

            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment; filename=" + "Daily_Income_Detail.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }
    }
}


