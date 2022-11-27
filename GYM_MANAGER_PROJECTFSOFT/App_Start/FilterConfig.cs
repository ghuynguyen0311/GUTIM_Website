using System.Web;
using System.Web.Mvc;

namespace GYM_MANAGER_PROJECTFSOFT
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
