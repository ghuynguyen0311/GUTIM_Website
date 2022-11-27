using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace GYM_MANAGER_PROJECTFSOFT.Models
{
    public class ProfileDetail
    {
        public int MaTaiKhoan { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }

        public Nullable<bool> Role { get; set; }

        //[EmailAddress(ErrorMessage = "The email address is not valid")]

        public string Email { get; set; }
        public string HoTen { get; set; }
        public string Avatar { get; set; }
        public string DiaChi { get; set;  }

        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string SoDienThoai { get; set; }
        public Nullable<DateTime> NgaySinh { get; set; }
        public int MaThanhVien { get; set; }
    }
}