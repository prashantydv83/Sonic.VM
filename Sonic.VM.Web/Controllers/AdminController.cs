using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Sonic.VM.Contracts;
using Sonic.VM.Entities;

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

            List<TotalSales> totalSales = new List<TotalSales>();
            TotalSales sale = new TotalSales();
            foreach (var p in paymenttypes)
            {
                sale = new TotalSales() { PaymentDesc = p.PaymtType, TotalAmount = _paymentDetailService.GetSalesByType(p.PaymtId) };
                totalSales.Add(sale);
            }
            
            ViewData["currentstock"] = currentstock;
            ViewData["totalsales"] = totalSales;
            return View();
        }

        public ActionResult Restock()
        {
            _paymentDetailService.RefreshPayments();
            _orderService.ResetOrders();
            var currentstock = _productService.GetProducts();
            var stockToBeAdded = (from product in currentstock
                            select new Product
                            {
                                ProdId = product.ProdId,
                                ProdQty = 5
                            }).ToList();

            _productService.AddNewProductStock(stockToBeAdded);

            var paymenttypes = _paymentDetailService.GetPaymentTypes();

            List<TotalSales> totalSales = new List<TotalSales>();
            TotalSales sale = new TotalSales();
            foreach (var p in paymenttypes)
            {
                sale = new TotalSales() { PaymentDesc = p.PaymtType, TotalAmount = _paymentDetailService.GetSalesByType(p.PaymtId) };
                totalSales.Add(sale);
            }

            var newStock = _productService.GetProducts();

            ViewData["currentstock"] = newStock;
            ViewData["totalsales"] = totalSales;
           
            return View();
        }
    }
}