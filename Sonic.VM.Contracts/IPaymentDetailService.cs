using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sonic.VM.Entities;

namespace Sonic.VM.Contracts
{
    public interface IPaymentDetailService
    {
        /// <summary>
        /// Get total sales by payment type.
        /// </summary>
        /// <param name="paymtId">paymtId</param>
        /// <returns>Sum of total sales by payment type</returns>
        double GetSalesByType(int paymtId);

        /// <summary>
        /// create a payment entry.
        /// </summary>
        /// <param name="PaymentDetail">paymentDetail</param>
        /// <returns>whethe payment successful or not</returns>
        bool AddPayment(PaymentDetail paymentDetail);

        /// <summary>
        /// Clear payment log
        /// </summary>
        void RefreshPayments();

        /// <summary>
        /// Get all payment types available
        /// </summary>       
        /// <returns>All Payment Types</returns>
        List<PaymentType> GetPaymentTypes();
    }
}
