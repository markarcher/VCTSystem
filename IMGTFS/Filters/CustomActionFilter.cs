using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace IMGTFS.Filters
{
    public class CustomActionFilter : ActionFilterAttribute, IActionFilter
    {

        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.CustomActionMessage1 = "Custom Action Filter: Message from OnActionExecuting method.";
                
        }
    }
}