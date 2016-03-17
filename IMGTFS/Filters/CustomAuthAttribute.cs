using IMGTFS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace IMGTFS.Filters
{
    public class CustomAuthAttribute :ActionFilterAttribute
    {
        //protected string Username { get; set; }
        //protected string Password { get; set; }
        public string Department { get; set; }
        //public CustomAuthAttribute(string username, string password)
        //{
         
        //    this.Username = username;
        //    this.Password = password;
        //}

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["userIsValid"] != null) { 
            Boolean validUserName = (Boolean)filterContext.HttpContext.Session["userIsValid"];
            string dept = (string)filterContext.HttpContext.Session["Department"];
            filterContext.Controller.ViewBag.CustomActionMessage1 = "CustomAuthAttribute OnAuthorization filter called." + Department;
                    if (validUserName == true && Department == dept){
                        return;
                    }
                    else { 
                        RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                        redirectTargetDictionary.Add("area", "");
                        redirectTargetDictionary.Add("id", 401);
                        redirectTargetDictionary.Add("action", "UnAuthorizedFilter");
                        redirectTargetDictionary.Add("controller", "ErrorPage");
                        filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);   
                    }
            }else{                  
                RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                redirectTargetDictionary.Add("area", "");
                redirectTargetDictionary.Add("id", 401);
                redirectTargetDictionary.Add("action", "UnAuthorizedFilter");
                redirectTargetDictionary.Add("controller", "ErrorPage");
                filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);   
            }

            base.OnActionExecuting(filterContext);
           
        }
    }
}