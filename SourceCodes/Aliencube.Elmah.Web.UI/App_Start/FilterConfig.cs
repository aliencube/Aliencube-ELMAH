using System.Web;
using System.Web.Mvc;
using Aliencube.Elmah.Mvc.Filters;

namespace Aliencube.Elmah.Web.UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ElmahExceptionFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}