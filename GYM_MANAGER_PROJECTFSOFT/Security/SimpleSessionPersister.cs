using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GYM_MANAGER_PROJECTFSOFT.Security
{
    public static class SimpleSessionPersister
    {
        static string usernameSessionvar = "taikhoan1";

        public static string Username
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return string.Empty;
                }
                var sessionVar = HttpContext.Current.Session[usernameSessionvar];
                if (sessionVar != null)
                {
                    return sessionVar as string;
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[usernameSessionvar] = value;
            }
        }
    }
}