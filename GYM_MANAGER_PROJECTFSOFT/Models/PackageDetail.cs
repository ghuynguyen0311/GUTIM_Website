using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GYM_MANAGER_PROJECTFSOFT.Models
{
    public class PackageDetail
    {
        public int MaGoiTap { get; set; }
        public string TenGoiTap { get; set; }
        public Nullable<int> ThoiHanGoiTap { get; set; }
        public Nullable<decimal> GiaGoiTap { get; set; }
        public string TenDichVu { get; set; }
        public string TenClub { get; set; }
    }
}