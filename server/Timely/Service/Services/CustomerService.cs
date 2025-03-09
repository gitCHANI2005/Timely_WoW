using Service.Dtos;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CustomerService : IService<CustomerDto>
    {
        public CustomerDto addItem(CustomerDto item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<CustomerDto> getAll()
        {
            throw new NotImplementedException();
        }

        public CustomerDto getById(int id)
        {
            throw new NotImplementedException();
        }

        public CustomerDto Update(int id, CustomerDto item)
        {
            throw new NotImplementedException();
        }
    }
}
