using IMG_BusinessEntities;
using IMG_BusinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMGTFS.Controllers
{
    public class LoginController : Controller
    {

         private ILoginService _loginService;

         public LoginController(ILoginService _loginService)
        {
            this._loginService = _loginService;
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            Session.Remove("userIsValid");
            return View();
        }

        public ActionResult ValidateUser(FormCollection collections)
        {
            string userName = collections["Username"];
            string password = collections["Password"];
            Boolean validUser;
            Users user = new Users();
            try
            {
                user = _loginService.ValidateUser(userName, password);
                Session["Department"] = user.department;
                validUser = _loginService.ProcessUser(user, userName, password);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Oops", "ErrorPage", new { id = 401, exceptionDescription = ex.InnerException.Message.ToString() });
            }
            Session["userIsValid"] = validUser;
            return RedirectToAction("Home", "Home");
            //  return RedirectToAction("~/Error");
        }


        public ActionResult Register(FormCollection collections)
        {
            string userName = collections["username"];
            string password = collections["password"];
            string fullName = collections["fullName"];
            string department = collections["department"];
            Users user = new Users()
            {
                userName = userName,
                password = password,
                fullName = fullName,
                department = department
            };
            int noOfItemsSaved = 0;
            try
            {
                noOfItemsSaved = _loginService.RegisterUser(user);
                if (noOfItemsSaved != 0)
                {
                    TempData["RegistrationComplete"] = "Thank you for registering please log in with your credentials";
                }
                else
                {
                    TempData["RegistrationComplete"] = "Registration Unsuccesful!";
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Oops", "ErrorPage", new { id = 400, exceptionDescription = ex.InnerException.Message.ToString() });

            }

            return RedirectToAction("Login", "Login");
        }

    }
}