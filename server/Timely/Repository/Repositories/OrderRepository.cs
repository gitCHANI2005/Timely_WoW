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
        private readonly IContext _context;
        public OrderRepository(IContext context)
        {
            _context = context;
        }

        public Order AddItem(Order item)
        {

            _context.Orders.Add(item);
            _context.save();
            return item;
        }

        public void DeleteItem(int id)
        {
            _context.Orders.Remove(Get(id));
            _context.save();
        }

        public Order Get(int id)
        {
            return _context.Orders.FirstOrDefault(x => x.Id == id);
        }

        public List<Order> GetAll()
        {
            return _context.Orders.ToList();
        }

        public int GetIdByName(string name)
        {
            throw new NotImplementedException();
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
            _context.save();
            return order;
        }
    }
}
