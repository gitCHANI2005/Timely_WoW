using Autofac.Features.OwnedInstances;
using AutoMapper;
using Repository.Entity;
using Repository.Interfaces;
using Repository.Repositories;
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
        private readonly IRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(IRepository<Customer> customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

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

        public Owner RegisterOwner(OwnerDto owner)
        {
            throw new NotImplementedException();
        }

        public CustomerDto Update(int id, CustomerDto item)
        {
            throw new NotImplementedException();
        }
        public Customer RegisterCustomer(CustomerDto customer)
        {
            Customer c = _customerRepository.AddItem(_mapper.Map<CustomerDto, Customer>(customer));
            return c;
        }

        public Deliver RegisterDeliver(DeliverDto item)
        {
            throw new NotImplementedException();
        }
    }
}
