using Autofac.Features.OwnedInstances;
using AutoMapper;
using Repository.Entity;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Dtos;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CustomerService : IService<CustomerDto>, IRegisterUser<Customer, CustomerDto>
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;
        private readonly IContext _context;

        public CustomerService(IRepository<Customer> customerRepository, IMapper mapper, IContext context)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _context = context;
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

        public CustomerDto Update(int id, CustomerDto item)
        {
            throw new NotImplementedException();
        }
        //public Customer RegisterCustomer(CustomerDto customer)
        //{
        //    Customer c = _customerRepository.AddItem(_mapper.Map<CustomerDto, Customer>(customer));
        //    return c;
        //}

        public Customer RegisterUser(CustomerDto item)
        {
            // שליפת רשימת הערים מהמסד
            var cities = _context.Cities.ToList();

            // מיפוי ראשוני של CustomerDto ל- Customer
            var customer = _mapper.Map<Customer>(item);

            // התאמת הערים לפי שם העיר המתקבל מה-DTO
            customer.CityHome = cities.FirstOrDefault(c => c.Name == item.CityHome);
            customer.CityIdHome = customer.CityHome?.Id;

            customer.CityWork = cities.FirstOrDefault(c => c.Name == item.CityWork);
            customer.CityIdWork = customer.CityWork?.Id;

            // בדיקה האם נמצאו הערים הרצויות
            if (customer.CityHome == null)
                throw new Exception($"CityHome '{item.CityHome}' not found in database.");
            if (customer.CityWork == null)
                throw new Exception($"CityWork '{item.CityWork}' not found in database.");

            // הוספת הלקוח למסד הנתונים
            return _customerRepository.AddItem(customer);
        }


    }
}
