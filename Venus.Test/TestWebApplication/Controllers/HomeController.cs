using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Venus.Application.Entity;
using Venus.Data.Repository;

namespace TestWebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
          return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult GetPersons()
        {
            try
            {
                IRepository TestRepository = new RepositoryFactory().BaseRepository();

                Expression<Func<TestEntity, bool>> expression = param => true;
                var result = TestRepository.FindEntity<TestEntity>(expression);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}