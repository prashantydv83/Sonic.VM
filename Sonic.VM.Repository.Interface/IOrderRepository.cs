using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sonic.VM.Entities;

namespace Sonic.VM.Repository.Interface
{
    public interface IOrderRepository
    {
        List<Order> GetOrders();
        bool PlaceOrder(Order order);
        void ResetOrders();
    }
}
