using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace IMGTFS.Filters
{
    public class CustomAuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.CustomActionMessage1 = "CustomAuthenticationAttribute OnactionExecuting filter called. YES!";
            try
            {
                if (!Authenticated(filterContext))
                {
                    RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                    redirectTargetDictionary.Add("area", "");
                    redirectTargetDictionary.Add("id", 401);
                    redirectTargetDictionary.Add("action", "UnAuthorizedFilter");
                    redirectTargetDictionary.Add("controller", "ErrorPage");
                    filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);   
                }
                else
                {
                    return;
                }           
                base.OnActionExecuting(filterContext);
            }
            catch
            {
                RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                redirectTargetDictionary.Add("area", "");
                redirectTargetDictionary.Add("id", 401);
                redirectTargetDictionary.Add("action", "UnAuthorizedFilter");
                redirectTargetDictionary.Add("controller", "ErrorPage");
                filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);  
            }
        }

        private bool Authenticated(ActionExecutingContext filterContext)
        {
            bool authenticated = false;
            if (filterContext.HttpContext.Session["userIsValid"] != null)
            {
                Boolean validUserName = (Boolean)filterContext.HttpContext.Session["userIsValid"];
                if (validUserName)
                {
                    authenticated = true;
                }
                else
                {
                    authenticated = false;
                }
            }

            return authenticated;
        }
    }
}