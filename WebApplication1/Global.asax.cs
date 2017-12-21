using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApplication1.Models;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest()
        {

            using (var db = new db_context())
            {
                db.HistoriaTable.Add(new Historia()
                {
                    Date = DateTime.Now,
                    Action = HttpContext.Current.Request.Url.OriginalString
                });

                db.SaveChanges();
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            HttpContext.Current.Session["color"] = "#ffffff";
        }


        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            Session["OstatniBlad"] = ex.Message;
        }
    }
}
