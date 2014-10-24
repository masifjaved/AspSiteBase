using AspSiteBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AspSiteBase.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseContext DbCon = new DatabaseContext();

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            var contacts = DbCon.Contacts;
            return View(contacts.ToList());
        }
    }
}