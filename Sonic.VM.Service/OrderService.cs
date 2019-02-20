using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sonic.VM.Entities;
using Sonic.VM.Contracts;
using Sonic.VM.Logging;
using System.Reflection;
using Sonic.VM.Repository.Data;
using Sonic.VM.Repository.Interface;

namespace Sonic.VM.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductService _productService;
        private readonly IPaymentDetailService _paymentDetailService;
        private readonly ILogger _logger = Logger.MyLogger;

        public OrderService(IOrderRepository orderRepository, IProductService productService, IPaymentDetailService paymentDetailService)
        {
            _orderRepository = orderRepository ?? new OrderDataRepository();
            _productService = productService;
            _paymentDetailService = paymentDetailService;
        }

        public List<Order> GetOrders()
        {
            _logger.LogInfoMessage("{0}()", MethodBase.GetCurrentMethod().Name);

            try
            {
                return _orderRepository.GetOrders();
            }
            catch (Exception ex)
            {
                _logger.LogInfoMessage("Exception: {0}", ex.Message);
                if (ex.InnerException != null) _logger.LogInfoMessage("Inner Exception: {0}", ex.InnerException.Message);
                throw new Exception();
            }
        }

        public bool PlaceOrder(Order order)
        {
            _logger.LogInfoMessage("{0}()", MethodBase.GetCurrentMethod().Name);

            try
            {
                //Validate Order Qty
                if (_productService.IsProductInStock(order.ProdId))
                {  
                    //AcceptPayment
                    PaymentDetail paymentDetail = new PaymentDetail {
                        PaymtDtlId = order.OrdrId,
                        PaymtID = order.PaymtDtlId,
                        Amnt = order.Amnt,
                        PaymtCardNo = "XXXX"
                    };
                    _paymentDetailService.AddPayment(paymentDetail);
                    
                    if (_orderRepository.PlaceOrder(order))
                    {
                        //Reduce Stock of the Product
                        return _productService.UpdateProductStock(order.ProdId);
                    }
                }
              
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogInfoMessage("Exception: {0}", ex.Message);
                if (ex.InnerException != null) _logger.LogInfoMessage("Inner Exception: {0}", ex.InnerException.Message);
                return false;
            }
        }

        public void ResetOrders()
        {
            _logger.LogInfoMessage("{0}()", MethodBase.GetCurrentMethod().Name);

            try
            {
                _orderRepository.ResetOrders();
            }
            catch (Exception ex)
            {
                _logger.LogInfoMessage("Exception: {0}", ex.Message);
                if (ex.InnerException != null) _logger.LogInfoMessage("Inner Exception: {0}", ex.InnerException.Message);
                throw new Exception();
            }
        }
    }
}
