using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMGTFS.Controllers
{
    public class ErrorPageController : Controller
    {
        // GET: ErrorPage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Oops(int id, string exceptionDescription)
        {
            Response.StatusCode = id;
            if (exceptionDescription != null)
            ViewBag.ExceptionMessage = exceptionDescription.ToString();

            return View();
        }

        public ActionResult UnAuthorizedFilter(int id)
        {
            Response.StatusCode = id;
            return View();
        }
    }
}