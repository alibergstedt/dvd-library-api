using DvdLibrary.Data.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DvdLibrary.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = DvdRepositoryFactory.GetRepository().GetAll();
            return View(model);
        }
    }
}