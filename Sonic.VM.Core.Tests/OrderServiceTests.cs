using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sonic.VM.Repository.Interface;
using Sonic.VM.Entities;
using Sonic.VM.Service;
using System.Collections.Generic;
using Moq;
using System;

namespace Sonic.VM.Core.Tests
{
    [TestClass]
    public class OrderServiceTests
    {
        private IOrderRepository orderRepository;

        [TestInitialize]
        public void Setup()
        {

            List<Order> orders = new List<Order> {
                 new Order{ OrdrId = 1, PaymtDtlId = 1, ProdId = 1, Amnt = 2.5, Qty = 1 },
             new Order{ OrdrId = 2, PaymtDtlId = 2, ProdId = 1, Amnt = 2.5, Qty = 1 }
            };

            Order ordr = new Order { OrdrId = 2, PaymtDtlId = 2, ProdId = 1, Amnt = 2.5, Qty = 1 };

            var _ordrrepoMock = new Mock<IOrderRepository>();
            _ordrrepoMock.Setup(r => r.GetOrders())
                .Returns(orders);
            _ordrrepoMock.Setup(r => r.PlaceOrder(It.IsAny<Order>()))
               .Returns(true);
            _ordrrepoMock.Setup(r => r.ResetOrders());
            orderRepository = _ordrrepoMock.Object;
        }


        [TestMethod]
        public void Orders_OnGetOrders_IsPopulated()
        {
            // Act
            var obj = orderRepository.GetOrders();

            // Assert
            Assert.IsNotNull(obj);
            Assert.AreEqual(2, obj.Count);
        }


    }
}
