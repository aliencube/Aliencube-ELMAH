using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aliencube.Elmah.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// GET: /home
        /// </summary>
        /// <returns>Returns the <c>ViewResult</c> instance.</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// GET: /home/confused
        /// </summary>
        /// <returns>Returns the <c>ViewResult</c> instance.</returns>
        /// <exception cref="NotImplementedException">Throws the <c>NotImplementedException</c> instance.</exception>
        public ActionResult Confused()
        {
            throw new NotImplementedException();
        }

    }
}
