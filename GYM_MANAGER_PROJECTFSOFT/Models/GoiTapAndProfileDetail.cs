using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GYM_MANAGER_PROJECTFSOFT.Models
{
    public class GoiTapAndProfileDetail
    {
        //Package Detail

        public int MaDichVu { get; set; }
        public int MaGoiTap { get; set; }

        public string TenDichVu { get; set; }
        public string TenGoiTap { get; set; }
        public Nullable<DateTime> NgayDangKy { get; set; }
        public Nullable<int> ThoiHanGoiTap { get; set; }
        public Nullable<decimal> GiaGoiTap { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public string DiaChiDichVu { get; set; }
        
        // Profile User
        
        public int MaTaiKhoan { get; set; }
        public int MaProfile { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }

        // Coupon
        public string Coupon { get; set; }
    }
}