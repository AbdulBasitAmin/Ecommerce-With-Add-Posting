using System.Web;
using System.Web.Mvc;

namespace ECommerce_With_AD_Posting
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
