using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GYM_MANAGER_PROJECTFSOFT.Models
{
    public class ExportExcelModel
    {
        public int MaDangKyGoiTap { get; set; }
        public string CodeThe { get; set; }
        public Nullable<System.DateTime> NgayDangKi { get; set; }

        public string TenGoiTap { get; set; }   
        public string TenClub { get; set; }

        public Nullable<decimal> GiaDangKi { get; set; }
    }
}