using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GYM_MANAGER_PROJECTFSOFT.Models
{
    public class Model1GroupTheThanhVien
    {
        public int MaTaiKhoan { get; set; }
        public int MaThanhVien { get; set; }
        public string HoTen { get; set; }
        public int MaTheThanhVien { get; set; }
        public Nullable<System.DateTime> NgayDangKy { get; set; }
        public Nullable<System.DateTime> NgayHetHan { get; set; }
        public string CodeThe { get; set; }
        public string TenLoai { get; set;}
        public string HinhAnhThe { get; set; }
    }
}