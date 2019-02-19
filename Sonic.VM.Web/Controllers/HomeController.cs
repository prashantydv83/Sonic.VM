using Sonic.VM.Contracts;
using Sonic.VM.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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
            _paymentDetailService = paymentDetailService;
            _orderService = orderService;
        }

        public ActionResult Index()
        {
            ViewData["SelectedProduct"] = null;
            var productList = _productService.GetProducts();

            var products = productList;

            return View(products);
        }

        public ActionResult Order(int prodId)
        {
            var products = _productService.GetProducts();
            var product = products.FirstOrDefault(x => x.ProdId == prodId);
            IList<PaymentType> paymentTypes=null;

            if (product != null)
            {
                if (product.ProdQty < 1)
                {
                    ViewBag.Message = "The selected item is out of stock. Please try again and select some other item.";
                    return View();
                }
                ViewData["SelectedProduct"] = product;
                paymentTypes = _paymentDetailService.GetPaymentTypes();
            }
            else
            {
                ViewBag.InvalidProductMessage = "Invalid Product";
            }

            return View(paymentTypes);
        }

        public ActionResult PlaceOrder(int id,int prodId)
        {
            var products = _productService.GetProducts();
            var product = products.FirstOrDefault(x => x.ProdId == prodId);

            if (product != null)
            {
                

                Order ordr = new Order { OrdrId = 10, PaymtDtlId = id, ProdId = product.ProdId, Amnt = product.ProdPrice, Qty = 1 };
                if (_orderService.PlaceOrder(ordr))
                {
                    ViewBag.Message = "Your payment has been accepted. Kindly collect your order. Thankyou!";
                }
                else
                {
                    ViewBag.Message = "There was a problem with your order. Please try again.";
                }
            }
            else
            {
                ViewBag.Message = "There was a problem with your order. Please try again.";
            }

            return View();
        }
    }
}