using AutoMapper;
using Microsoft.Extensions.Configuration;
using Repository.Entity;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Dtos;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly IContext _context;
        private readonly JwtService _jwtService;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Deliver> _deliverRepository;
        private readonly IRepository<Owner> _ownerRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IMapper mapper;



        public UserService(IContext context, JwtService jwtService, IConfiguration configuration, IRepository<Customer> customerRepository, IRepository<Deliver> deliverRepository, IRepository<Owner> ownerRepository, IRepository<City> cityRepository,IMapper map)
        {
            _context = context;
            _jwtService = jwtService;
            _configuration = configuration;
            _customerRepository = customerRepository;
            _deliverRepository = deliverRepository;
            _ownerRepository = ownerRepository;
            _cityRepository = cityRepository;
            mapper = map;
        }

        public UserDto ValidateUser(string email, string password)
        {
            var user = FindCustomer(email, password) ??
                       FindDeliverer(email, password) ??
                       FindOwner(email, password) ??
                       FindAdmin(email, password);
            return user;
        }

        private UserDto FindCustomer(string email, string password)      
        {
            var customer = _context.Customers.SingleOrDefault(x => x.Email == email && x.Password == password);
            if (customer != null)
            {
                return new UserDto
                {
                    Email = customer.Email,
                    Role = Roles.customer,
                    Id = customer.Id,              
                };
            }
            return null;
        }

        private UserDto FindDeliverer(string email, string password)
        {
            var deliver = _context.Delivers.FirstOrDefault(x => x.Email == email && x.Password == password);
            if (deliver != null)
            {
                return new UserDto
                {
                    Email = deliver.Email,
                    Role = Roles.deliver,
                    Id = deliver.Id,

                };
            }
            return null;
        }

        private UserDto FindOwner(string email, string password)
        {
            var owner = _context.Owners.FirstOrDefault(x => x.Email == email && x.Password == password);
            if (owner != null)
            {
                return new UserDto
                {
                    Email = owner.Email,
                    Role = Roles.owner,
                    Id = owner.Id,

                };
            }
            return null;
        }

        private UserDto FindAdmin(string email, string password)
        {
            var admin = _configuration.GetSection("Admin");
            var adminEmail = admin["Email"];
            var adminPassword = admin["Password"];
            var adminId = admin["id"];
            Roles adminRole = Roles.admin;

            if (email == adminEmail && password == adminPassword)
            {
                 return new UserDto
                 {
                      Email = email,
                      Role = adminRole,
                      //Id = adminId,
                 };
            }          
              return null;
        }

        public bool IsEmailTaken(string email)
        {
            return _customerRepository.GetAll().Any(c => c.Email == email) ||
                   _deliverRepository.GetAll().Any(d => d.Email == email) ||
                   _ownerRepository.GetAll().Any(o => o.Email == email);
        }

    }

}
