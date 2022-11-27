using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using GYM_MANAGER_PROJECTFSOFT.Models;

namespace GYM_MANAGER_PROJECTFSOFT.Models
{
    public class CustomPrincipal : IPrincipal
    {
        GYM_ManangerEntities db = new GYM_ManangerEntities();
        public IIdentity Identity
        {
            get;
            set;
        }

        private TAIKHOAN taikhoan;

        public CustomPrincipal(TAIKHOAN taikhoan)
        {
            this.taikhoan = taikhoan;
            this.Identity = new GenericIdentity(taikhoan.TaiKhoan1);
        }

        public bool IsInRole(string role)
        {
            var roles = role;
            if (roles == "true")
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}