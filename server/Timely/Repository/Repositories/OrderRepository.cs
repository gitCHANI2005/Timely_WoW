using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    internal class OrderRepository : IRepository<Order>
    {
        private readonly IContext context;

        public Order AddItem(Order item)
        {
            context.Orders.Add(item);
            context.save();
            return item;
        }

        public void DeleteItem(int id)
        {
            context.Orders.Remove(Get(id));
            context.save();
        }

        public Order Get(int id)
        {
            return context.Orders.FirstOrDefault(x => x.Id == id);
        }

        public List<Order> GetAll()
        {
            return context.Orders.ToList();
        }

        public Order UpdateItem(int id, Order item)
        {
            Order order = Get(id);
            order.CustomerId = item.CustomerId; 
            order.customer = item.customer;
            order.DeliverId=item.DeliverId;
            order.deliver = item.deliver;
            order.StoreId = item.StoreId;
            order.store = item.store;
            order.OrderDate= item.OrderDate;
            order.FinalPrice= item.FinalPrice;
            order.status=item.status;
            context.save();
            return order;
        }
    }
}
