using Repository.Entity;
using Service.Dtos;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class OrderService : IService<OrderDto>
    {
        public OrderDto addItem(OrderDto item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<OrderDto> getAll()
        {
            throw new NotImplementedException();
        }

        public OrderDto getById(int id)
        {
            throw new NotImplementedException();
        }

        public Customer RegisterCustomer(CustomerDto customer)
        {
            throw new NotImplementedException();
        }

        public OrderDto RegisterDeliver(DeliverDto item)
        {
            throw new NotImplementedException();
        }

        public Deliver RegisterDeliver(OrderDto item)
        {
            throw new NotImplementedException();
        }

        public Owner RegisterOwner(OwnerDto owner)
        {
            throw new NotImplementedException();
        }

        public OrderDto Update(int id, OrderDto item)
        {
            throw new NotImplementedException();
        }

        Deliver IService<OrderDto>.RegisterDeliver(DeliverDto item)
        {
            throw new NotImplementedException();
        }
    }
}
