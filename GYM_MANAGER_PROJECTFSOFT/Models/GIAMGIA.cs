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
    
    public partial class GIAMGIA
    {
        public int MaGiamGia { get; set; }
        public Nullable<int> MaGoiTap { get; set; }
        public string CodeGiamGia { get; set; }
        public Nullable<decimal> SoTienGiamGia { get; set; }
        public string Soluong { get; set; }
    
        public virtual GOITAP GOITAP { get; set; }
    }
}
