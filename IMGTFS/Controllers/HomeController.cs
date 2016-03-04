using IMG_BusinessServices;
using IMGTFS.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace IMGTFS.Controllers
{
  
    [CustomActionFilter]
    public class HomeController : Controller
    {

        private ILoginService _loginService;

        public HomeController(ILoginService _loginService)
        {
            this._loginService = _loginService;
        }
        //
        // GET: /Home/
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Login()
        {
            ViewBag.registered = TempData["registerMessage"];
            return View();
        }

        public ActionResult ValidateUser(FormCollection collections)
        {

            return View();
        }

        public ActionResult Register(FormCollection collections)
        {
            //string userName = collections["Username"];
            //string password = collections["Password"];

            //_loginService.ValidateUser(userName, password);
            TempData["registerMessage"] = "Thank you for registering please log in with your credentials";
           
            return RedirectToAction("Login","Home");
        }
	}
}