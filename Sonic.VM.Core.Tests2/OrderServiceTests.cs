using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sonic.VM.Repository.Interface;
using Sonic.VM.Entities;
using Sonic.VM.Service;
using System.Collections.Generic;
using Moq;
using System;

namespace Sonic.VM.Core.Tests2
{
    [TestClass]
    public class OrderServiceTests
    {
        private IOrderRepository orderRepository;
        IPaymtDetailRepository paymtDetailRepository;
        IProductRepository productRepository;
        OrderService orderService;
        List<Order> orders;
        Order ordr;

        [TestInitialize]
        public void Setup()
        {

            orders = new List<Order> {
                 new Order{ OrdrId = 1, PaymtDtlId = 1, ProdId = 1, Amnt = 2.5, Qty = 1 },
             new Order{ OrdrId = 2, PaymtDtlId = 2, ProdId = 1, Amnt = 2.5, Qty = 1 }
            };

            ordr = new Order { OrdrId = 2, PaymtDtlId = 2, ProdId = 1, Amnt = 2.5, Qty = 1 };

            var _ordrrepoMock = new Mock<IOrderRepository>();
            var _productrepoMock = new Mock<IProductRepository>();
            var _paymtrepoMock = new Mock<IPaymtDetailRepository>();
            _ordrrepoMock.Setup(r => r.GetOrders())
                .Returns(orders);
            _ordrrepoMock.Setup(r => r.PlaceOrder(It.IsAny<Order>()))
               .Returns(true);
            _ordrrepoMock.Setup(r => r.ResetOrders());
            orderRepository = _ordrrepoMock.Object;
            paymtDetailRepository = _paymtrepoMock.Object;
            productRepository = _productrepoMock.Object;
            orderService = new OrderService(_ordrrepoMock.Object, new ProductService(productRepository), new PaymentDetailService(paymtDetailRepository));
        }


        [TestMethod]
        public void Orders_OnGetOrders_IsPopulated()
        {
            // Act
            var result = orderService.GetOrders();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void InValidOrder_OnPlaceOrders_IsNotPopulated()
        {
            // Act
            var result = orderService.PlaceOrder(ordr);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void On_ResetOrder_Works()
        {
            // Act
            orderService.ResetOrders();
           
        }


    }
}
