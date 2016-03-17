using IMG_BusinessServices;
using IMGTFS.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMG_BusinessEntities;
using System.Web.Security;

namespace IMGTFS.Controllers
{



    [CustomAuthentication]
    public class HomeController : Controller
    {      
        //
        // GET: /Home/
      
        public ActionResult Home()
        {
            return View();
        } 
	}
}