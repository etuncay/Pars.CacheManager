using System.Web.Mvc;
using Pars.CacheManager;

namespace ExampleWebProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var cache = CacheProvider.Creator();

            cache.Add("anahtarKelime", "Kelime");

            var get = cache.Get<string>("anahtarKelime");



            return Content("");
        }
    }
}