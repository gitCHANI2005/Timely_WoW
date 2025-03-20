using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly IContext context;
        public CustomerRepository(IContext context)
        {
            this.context = context;
        }

        public Customer AddItem(Customer item)
        {
            context.Customers.Add(item);
            context.save();
            return item;
        }

        public void DeleteItem(int id)
        {
            context.Customers.Remove(Get(id));
            context.save();
        }

        public Customer Get(int id)
        {
            return context.Customers.FirstOrDefault(x => x.Id == id);

        }

        public List<Customer> GetAll()
        {
            return context.Customers.ToList();

        }

        public int GetIdByName(string name)
        {
            throw new NotImplementedException();
        }

        public Customer UpdateItem(int id, Customer item)
        {
            Customer customer = Get(id);
            customer.Id = id;
            customer.Name = item.Name;
            customer.Email = item.Email;
            customer.Phone = item.Phone;
            customer.AdressHome = item.AdressHome;
            customer.AdressWork=item.AdressWork;
            customer.CityWork=item.CityWork;
            customer.CityIdWork=item.CityIdWork;
            customer.CardCvv=item.CardCvv;
            customer.Identity=item.Identity;
            customer.CardNumber=item.CardNumber;
            context.save();
            return customer;
        }

    }
}
