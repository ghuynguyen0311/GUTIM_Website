using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GYM_MANAGER_PROJECTFSOFT.Models
{
    public class TheThanhVienDetail
    {
        public int MaTaiKhoan { get; set; }
        public int MaLoaiTheThanhVien { get; set; }
        public string TenThe { get; set; }
        public string CodeThe { get; set; }
        public Nullable<System.DateTime> NgayDangKy { get; set; }
        public Nullable<System.DateTime> NgayHetHan { get; set; }
        public string HinhAnhThe { get; set; }
    }
}