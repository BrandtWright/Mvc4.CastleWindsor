using System;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    using Models;

    public class HomeController : Controller
    {
        private readonly string _applicationName;

        public HomeController(string applicationName)
        {
            if (applicationName == null)
                throw new ArgumentNullException(
                    "applicationName", 
                    "Make sure you set the applicationName setting in your web.config file.");

            _applicationName = applicationName;
        }

        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View(new IndexModel{ApplicationName = _applicationName});
        }
    }
}
