using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GYM_MANAGER_PROJECTFSOFT.Models
{
    public class RegisterPackageDetail
    {
        public int MaDangKyGoiTap { get; set; }
        public string CodeThe { get; set; }
        public Nullable<System.DateTime> NgayDangKi { get; set; }
        public Nullable<decimal> GiaDangKi { get; set; }
        public string TenGoiTap { get; set; }

        public Nullable<bool> TrangThai { get; set; }
    }
}