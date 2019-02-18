using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sonic.VM.Entities;

namespace Sonic.VM.Repository.Interface
{
    public interface IPaymtDetailRepository
    {
        bool AddPayment(PaymentDetail paymentDetail);
        List<PaymentDetail> GetPaymentDetails();
        List<PaymentType> GetPaymentTypes();
        void RefreshPayments();
    }
}
