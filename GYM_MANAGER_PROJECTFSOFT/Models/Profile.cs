//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GYM_MANAGER_PROJECTFSOFT.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Profile
    {
        public int MaProfile { get; set; }
        public Nullable<int> MaTaiKhoan { get; set; }
        public string HoTen { get; set; }
        public string DiaChi { get; set; }
        public string Avatar { get; set; }
        public Nullable<System.DateTime> NgaySinh { get; set; }
        public Nullable<int> MaThanhVien { get; set; }
    
        public virtual THANHVIEN THANHVIEN { get; set; }
        public virtual TAIKHOAN TAIKHOAN { get; set; }
    }
}
