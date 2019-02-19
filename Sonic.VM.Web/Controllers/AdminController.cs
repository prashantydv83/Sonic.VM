using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Sonic.VM.Contracts;

namespace Sonic.VM.Web.Controllers
{
    public class AdminController : Controller
    {
        private IProductService _productService;
        private IPaymentDetailService _paymentDetailService;
        private IOrderService _orderService;

        public AdminController(IProductService productService, IPaymentDetailService paymentDetailService, IOrderService orderService)
        {
            _productService = productService;
            _paymentDetailService = paymentDetailService;
            _orderService = orderService;
        }

        public ActionResult Home()
        {
            var currentstock = _productService.GetProducts();
            var paymenttypes = _paymentDetailService.GetPaymentTypes();

            List<Models.TotalSales> totalSales = new List<Models.TotalSales>();
            Models.TotalSales sale = new Models.TotalSales();
            foreach (var p in paymenttypes)
            {
                sale = new Models.TotalSales() { PaymentDesc = p.PaymtType, TotalAmount = _paymentDetailService.GetSalesByType(p.PaymtId) };
                totalSales.Add(sale);
            }

            var query = (from product in currentstock
                         select new Models.Product
                         {
                             ProdId = product.ProdId,
                             ProdName = product.ProdName,
                             ProdPrice = product.ProdPrice,
                             ProdQty = product.ProdQty
                         }).Distinct().ToList<Models.Product>();

            IEnumerable<Models.Product> prod = query;
            ViewData["currentstock"] = prod;
            ViewData["totalsales"] = totalSales;
            return View();
        }

        public ActionResult Restock()
        {
            _paymentDetailService.RefreshPayments();
            _orderService.ResetOrders();
            //_productService.AddNewProductStock();

            var currentstock = _productService.GetProducts();
            var paymenttypes = _paymentDetailService.GetPaymentTypes();

            List<Models.TotalSales> totalSales = new List<Models.TotalSales>();
            Models.TotalSales sale = new Models.TotalSales();
            foreach (var p in paymenttypes)
            {
                sale = new Models.TotalSales() { PaymentDesc = p.PaymtType, TotalAmount = _paymentDetailService.GetSalesByType(p.PaymtId) };
                totalSales.Add(sale);
            }

            var query = (from product in currentstock
                         select new Models.Product
                         {
                             ProdId = product.ProdId,
                             ProdName = product.ProdName,
                             ProdPrice = product.ProdPrice,
                             ProdQty = product.ProdQty
                         }).Distinct().ToList<Models.Product>();

            IEnumerable<Models.Product> prod = query;
            ViewData["currentstock"] = prod;
            ViewData["totalsales"] = totalSales;
           
            return View();
        }
    }
}