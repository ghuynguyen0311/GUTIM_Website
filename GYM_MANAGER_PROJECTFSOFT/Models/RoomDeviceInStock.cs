using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GYM_MANAGER_PROJECTFSOFT.Models
{
    public class RoomDeviceInStock
    {
        public int MaThietBiTrongKho { get; set; }
        public Nullable<bool> TrangThaiTonKho { get; set; }
        public string TenThietBi { get; set; }
        public Nullable<bool> TrangThaiThietBi { get; set; }
        public string HinhThietBi { get; set; }
    }
}