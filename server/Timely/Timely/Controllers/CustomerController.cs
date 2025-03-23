using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using Repository.Entity;
using Repository.Interfaces;
using Service.Dtos;
using Service.Interfaces;
using Service.Services;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Timely.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IRepository<Customer> _customerrepository;
        private readonly JwtService _jwtService;
        //private readonly IService<Customer> _customerService;
        private readonly IRegisterUser<Customer, CustomerDto> _registerUser;

        public CustomerController(IRepository<Customer> customerrepository, JwtService jwtService, IRegisterUser<Customer, CustomerDto> registerUser)
        {
            _customerrepository = customerrepository;
            _jwtService = jwtService;
            //_customerService = customerService;
            _registerUser = registerUser;
        }

        [HttpPost]
        public Customer Register([FromBody] CustomerDto customer)
        {
            if (string.IsNullOrEmpty(customer.Email) || string.IsNullOrEmpty(customer.Password))
            {
                Console.WriteLine("Email, password, and role are required.");
            }

            Customer c = _registerUser.RegisterUser(customer);
            return c;
        }

        [HttpGet]
        public List<Customer> Get()
        {
            return _customerrepository.GetAll();
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public Customer Get(int id)
        {
            return _customerrepository.Get(id);
        }
        [HttpPut("{id}")]
        [Authorize]
        public Customer Put(int id, [FromBody] Customer updateCustomer)
        {
            var existingCustomer = _customerrepository.Get(id);
            _customerrepository.UpdateItem(id, updateCustomer);

            return existingCustomer;
        }


        [HttpDelete("{id}") ]
        [Authorize(Policy = "AdminOnly")]
        public void Delete(int id)
        {
            var existingCustomer = _customerrepository.Get(id);
            _customerrepository.DeleteItem(id);
        }
    }
}
