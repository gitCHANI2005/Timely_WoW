using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IOrders<T>
    {
        List<T> GetOrdersByDeliverId(int DeliverId);
    }
}
