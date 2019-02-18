using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sonic.VM.Contracts;

namespace Sonic.VM.Web.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public ActionResult Index()
        {
            var obj = _productService.GetProducts();

            var query = (from product in obj
                         select new Models.Product
                         {
                             ProdId = product.ProdId,
                             ProdName = product.ProdName,
                             ProdPrice = product.ProdPrice,
                             ProdQty = product.ProdQty                             
                         }).Distinct().ToList<Models.Product>();

            IEnumerable<Models.Product> prod = query;
            return View(prod);
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
    }
}