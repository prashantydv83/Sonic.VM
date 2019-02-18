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
    public class PaymentDetailServiceTests
    {
        IPaymtDetailRepository paymtDetailRepository;

        [TestInitialize]
        public void Setup()
        {
            
            List<PaymentType> paymentTypes = new List<PaymentType> {
                new PaymentType{ PaymtId = 1, PaymtType= "Cash" },
                new PaymentType{ PaymtId = 2, PaymtType= "Card" }
            };

            var _paymtrepoMock = new Mock<IPaymtDetailRepository>();
            _paymtrepoMock.Setup(r => r.AddPayment(It.IsAny<PaymentDetail>()))
                .Returns(true);
            _paymtrepoMock.Setup(r => r.GetPaymentTypes())
               .Returns(paymentTypes);
            
            _paymtrepoMock.Setup(r => r.RefreshPayments());             
            paymtDetailRepository = _paymtrepoMock.Object;
        }

        [TestMethod]
        public void PaymentDetail_OnAddPayment_IsPopulated()
        {
            //Arrange 
            PaymentDetail paymentDetail = new PaymentDetail { PaymtDtlId = 1, PaymtID = 1, Amnt = 2.5 };

            // Act
            var obj = paymtDetailRepository.AddPayment(paymentDetail);

            // Assert
            Assert.IsNotNull(obj);
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void NullPaymentDetail_OnAddPayment_IsPopulated()
        {
            //Arrange 
            PaymentDetail paymentDetail = new PaymentDetail ();

            // Act
            var obj = paymtDetailRepository.AddPayment(paymentDetail);

            // Assert
            Assert.IsTrue(obj);
        }

        [TestMethod]
        public void PaymentTypes_OnGetPaymentTypes_IsPopulated()
        {
            // Act
            var obj = paymtDetailRepository.GetPaymentTypes();

            // Assert
            Assert.IsNotNull(obj);
            Assert.AreEqual(2, obj.Count);
        }

        [TestMethod]
        public void OnRefreshPayments_Wokrs()
        {
            // Act
           paymtDetailRepository.RefreshPayments();

            // Assert
        }
    }
}
