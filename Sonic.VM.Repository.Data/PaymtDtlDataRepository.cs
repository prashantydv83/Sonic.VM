using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sonic.VM.Entities;
using Sonic.VM.Repository.Interface;

namespace Sonic.VM.Repository.Data
{
    public class PaymtDtlDataRepository : IPaymtDetailRepository
    {
        List<PaymentDetail> paymentDetails = new List<PaymentDetail>();
        
        List<PaymentType> paymentTypes = new List<PaymentType> {
            new PaymentType{ PaymtId = 1, PaymtType= "Cash" },
            new PaymentType{ PaymtId = 2, PaymtType= "Card" }
        };

        public bool AddPayment(PaymentDetail paymentDetail)
        {
            paymentDetails.Add(paymentDetail);
            return true;            
        }

        public List<PaymentDetail> GetPaymentDetails()
        {
            return paymentDetails;
        }

        public List<PaymentType> GetPaymentTypes()
        {
            return paymentTypes;
        }

        public void RefreshPayments()
        {
            paymentDetails.Clear();
        }
    }
}
