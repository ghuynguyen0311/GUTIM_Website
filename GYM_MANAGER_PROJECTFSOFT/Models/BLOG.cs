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
    
    public partial class BLOG
    {
        public int MaBlog { get; set; }
        public Nullable<int> MaLoaiBlog { get; set; }
        public string HinhAnh { get; set; }
        public string NoiDung { get; set; }
        public Nullable<System.DateTime> NgayDang { get; set; }
        public string TieuDe { get; set; }
    
        public virtual LOAIBLOG LOAIBLOG { get; set; }
    }
}