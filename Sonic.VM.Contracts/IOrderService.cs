using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sonic.VM.Entities;

namespace Sonic.VM.Contracts
{
    public interface IOrderService
    {
        /// <summary>
        /// Get all placed orders.
        /// </summary>
        /// /// <returns>List of all orders placed</returns>
        List<Order> GetOrders();

        /// <summary>
        /// Place an order for a user.
        /// </summary>
        /// <param name="order">order</param>
        /// <returns>whether order process is successful or not</returns>
        bool PlaceOrder(Order order);

        /// <summary>
        /// Clear order log.
        /// </summary>
        /// <returns></returns>
        void ResetOrders();
    }
}
