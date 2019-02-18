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
    public class PaymentDetailService : IPaymentDetailService
    {
        private readonly ILogger _logger = Logger.MyLogger;
        private readonly IPaymtDetailRepository paymtDtlRepository;

        public PaymentDetailService()
        {
            paymtDtlRepository = new PaymtDtlDataRepository();
        }

        public bool AddPayment(PaymentDetail paymentDetail)
        {
            _logger.LogInfoMessage("{0}()", MethodBase.GetCurrentMethod().Name);

            try
            {
                if (paymentDetail == null)
                    throw new ArgumentException();
                return paymtDtlRepository.AddPayment(paymentDetail);
            }
            catch (Exception ex)
            {
                _logger.LogInfoMessage("Exception: {0}", ex.Message);
                if (ex.InnerException != null) _logger.LogInfoMessage("Inner Exception: {0}", ex.InnerException.Message);
                return false;
            }
        }

        public List<PaymentType> GetPaymentTypes()
        {
            _logger.LogInfoMessage("{0}()", MethodBase.GetCurrentMethod().Name);

            var paymentTypes = new List<PaymentType>();

            try
            {
                return paymtDtlRepository.GetPaymentTypes();
            }
            catch (Exception ex)
            {
                _logger.LogInfoMessage("Exception: {0}", ex.Message);
                if (ex.InnerException != null) _logger.LogInfoMessage("Inner Exception: {0}", ex.InnerException.Message);
                throw new Exception();
            }
        }

        public double GetSalesByType(int paymtId)
        {
            _logger.LogInfoMessage("{0}()", MethodBase.GetCurrentMethod().Name);

            var paymentDetails = new List<PaymentDetail>();
            try
            {
                paymentDetails = paymtDtlRepository.GetPaymentDetails();
                return paymentDetails.Where(i => i.PaymtID == paymtId).Sum(a => a.Amnt);
            }
            catch (Exception ex)
            {
                _logger.LogInfoMessage("Exception: {0}", ex.Message);
                if (ex.InnerException != null) _logger.LogInfoMessage("Inner Exception: {0}", ex.InnerException.Message);
                throw new Exception();
            }
        }

        public void RefreshPayments()
        {
            _logger.LogInfoMessage("{0}()", MethodBase.GetCurrentMethod().Name);

            try
            {
                paymtDtlRepository.RefreshPayments();
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
