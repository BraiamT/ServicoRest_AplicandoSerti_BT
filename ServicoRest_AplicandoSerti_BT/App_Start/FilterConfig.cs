using System.Web;
using System.Web.Mvc;

namespace ServicoRest_AplicandoSerti_BT
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
