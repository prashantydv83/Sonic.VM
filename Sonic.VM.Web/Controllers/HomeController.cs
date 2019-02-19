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
        private IPaymentDetailService _paymentDetailService;
        private IOrderService _orderService;

        public HomeController(IProductService productService, IPaymentDetailService paymentDetailService, IOrderService orderService)
        {
            _productService = productService;
            _paymentDetailService= paymentDetailService;
            _orderService = orderService;
        }

        public ActionResult Index()
        {
            Session["SelectedProduct"] = null;
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

        public ActionResult Order(int ProdId)
        {
            var products = _productService.GetProducts();
            var query = (from product in products
                         where product.ProdId == ProdId
                         select new Models.Product
                         {
                             ProdId = product.ProdId,
                             ProdName = product.ProdName,
                             ProdPrice = product.ProdPrice,
                             ProdQty = product.ProdQty
                         }).Distinct();
            Session["SelectedProduct"] = query.First();
       
            var obj = _productService.IsProductInStock(ProdId);

            if (obj)
            {
                ViewBag.Message = "";
                var payment = _paymentDetailService.GetPaymentTypes();
                var paymentstype = (from pymt in payment
                             select new Models.PaymentType
                             {
                                 PaymtId = pymt.PaymtId,
                                 PaymtType = pymt.PaymtType
                             }).Distinct().ToList<Models.PaymentType>();
                IEnumerable<Models.PaymentType> prod = paymentstype;
                return View(prod);
            }
            else
            {
                ViewBag.Message = "No Stock Available. Please try some other product.";
            }
           
             return View();
        }

        public ActionResult PlaceOrder(Models.Order order)
        {
            Entities.Order ordr = new Entities.Order { OrdrId = -1, PaymtDtlId = order.PaymtDtlId, ProdId = order.ProdId, Amnt = order.Amnt, Qty = order.Qty };
            if (_orderService.PlaceOrder(ordr))
            {
                ViewBag.Message = "Your payment has been accepted. Kindly collect your order. Thankyou!";
            }
            
            return View();
        }
    }
}