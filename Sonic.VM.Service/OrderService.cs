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
        private readonly IOrderRepository orderRepository;
        private readonly ILogger _logger = Logger.MyLogger;

        public OrderService()
        {
            orderRepository = new OrderDataRepository();
        }

        public List<Order> GetOrders()
        {
            _logger.LogInfoMessage("{0}()", MethodBase.GetCurrentMethod().Name);

            try
            {
                return orderRepository.GetOrders();
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

                //Create Order

                //AcceptPayment
                PaymentDetail paymentDetail = new PaymentDetail();
                new PaymentDetailService().AddPayment(paymentDetail);

                //Reduce Stock of the Product
                return orderRepository.PlaceOrder(order);                

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
                orderRepository.ResetOrders();
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
