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



        public UserService(IContext context, JwtService jwtService, IConfiguration configuration, IRepository<Customer> customerRepository, IRepository<Deliver> deliverRepository, IRepository<Owner> ownerRepository)
        {
            _context = context;
            _jwtService = jwtService;
            _configuration = configuration;
            _customerRepository = customerRepository;
            _deliverRepository = deliverRepository;
            _ownerRepository = ownerRepository;
        }

        public UserDto ValidateUser(string email, string password)
        {
            // בדיקת כל סוגי המשתמשים
            var user = FindCustomer(email, password) ??
                       FindDeliverer(email, password) ??
                       FindOwner(email, password) ??
                       FindAdmin(email, password);

            return user;
        }

        private UserDto FindCustomer(string email, string password)
        
        {
            var customer = _context.Customers.FirstOrDefault(x => x.Email == email && x.Password == password);
            if (customer != null)
            {
                return new UserDto
                {
                    Email = customer.Email,
                    Role = Roles.customer,
                    Name = customer.Name,

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
                    Name = deliver.Name,
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
                    Name = owner.Name,

                };
            }
            return null;
        }

        private UserDto FindAdmin(string email, string password)
        {
            var admin = _configuration.GetSection("Admin");
            var adminEmail = admin["Email"];
            var adminPassword = admin["Password"];
            var adminName = admin["Name"];
            Roles adminRole = Roles.admin;

            if (email == adminEmail && password == adminPassword)
            {
                 return new UserDto
                 {
                      Email = email,
                      Role = adminRole,
                      Name = adminName
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

        public string RegisterDeliver(DeliverDto deliver)
        {
            string role = deliver.Role + "";
            string token;
            var d = new Deliver {
                Name = deliver.Name,
                Email = deliver.Email,
                Password = deliver.Password,
                Phone = deliver.Phone,
                Identity = deliver.Identity,
                Role = deliver.Role,
                DateOfBirth = deliver.DateOfBirth,
                IsActive = deliver.IsActive,
                CityId = deliver.CityId,
                NumOfCar = deliver.NumOfCar,
                BankNumber = deliver.BankNumber,
                BankAccount = deliver.BankAccount,
                BankBranch = deliver.BankBranch
            };
                    _deliverRepository.AddItem(d);
                    token = _jwtService.GenerateToken(deliver.Name, role, deliver.Email);
            return token;
        }

        public string RegisterCustomer(CustomerDto customer)
        {
            string role = customer.Role + "";
            string token;
            var c = new Customer
            {
                Name = customer.Name,
                Email = customer.Email,
                Role = customer.Role,
                Password = customer.Password,
                Phone = customer.Phone,
                Identity = customer.Identity,
                //CityIdHome = null,
                //CityIdWork = null,
                AdressHome = customer.AdressHome,
                AdressWork = customer.AdressWork,
                CardNumber = customer.CardNumber,
                CardValidity = customer.CardValidity,
                CardCvv = customer.CardCvv
            };
            _customerRepository.AddItem(c);
            token = _jwtService.GenerateToken(customer.Name, role, customer.Email);
            return token;
        }

        public string RegisterOwner(OwnerDto owner)
        {
            string role = owner.Role + "";
            string token;
            var o = new Owner
            {
                Name = owner.Name,
                Email = owner.Email,
                Role = owner.Role,
                Password = owner.Password,
                Phone = owner.Phone,
                Identity = owner.Identity,

            };
            _ownerRepository.AddItem(o);
            token = _jwtService.GenerateToken(owner.Name, role, owner.Email);
            return token;
        }
    }

}
