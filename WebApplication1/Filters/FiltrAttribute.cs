using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication1.Filters
{
    public class FiltrAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (string.IsNullOrEmpty(filterContext.HttpContext.Request.QueryString["ID"]))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary() { { "controller", "Home" }, { "action", "Index" } });
            }

            base.OnActionExecuting(filterContext);
        }
    }
}