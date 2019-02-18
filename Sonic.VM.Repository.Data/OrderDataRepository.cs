using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sonic.VM.Entities;
using Sonic.VM.Repository.Interface;

namespace Sonic.VM.Repository.Data
{
    public class OrderDataRepository : IOrderRepository
    {
        List<Order> orders = new List<Order>();

        public List<Order> GetOrders()
        {
            return orders;
        }

        public bool PlaceOrder(Order order)
        {
            orders.Add(order);
            return true;
        }

        public void ResetOrders()
        {
            orders.Clear();
        }
    }
}
