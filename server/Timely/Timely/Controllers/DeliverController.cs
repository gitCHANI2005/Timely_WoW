using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entity;
using Repository.Interfaces;
using Service.Dtos;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Timely.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class DeliverController : ControllerBase

    {
        private readonly IRepository<Deliver> _deliverrepository;
        private readonly IUserService _userService;
        private readonly IService<DeliverDto> _deliverService;


        public DeliverController(IRepository<Deliver> deliverrepository, IUserService userService, IService<DeliverDto> deliverService)
        {
            _deliverrepository = deliverrepository;
            _userService = userService;
            _deliverService = deliverService;   
        }

        [HttpPost]
        public Deliver Register([FromBody] DeliverDto deliver)
        {
            if (string.IsNullOrEmpty(deliver.Email) || string.IsNullOrEmpty(deliver.Password))
            {
                Console.WriteLine("gby"); ;
            }

            Deliver d = _deliverService.RegisterDeliver(deliver);
            return d;
        }

        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public List<Deliver> Get()
        {
            return _deliverrepository.GetAll();
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public Deliver Get(int id)
        {
            return _deliverrepository.Get(id);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "DeliverOnly")]
        public Deliver Put(int id, [FromBody] Deliver updateDeliver)
        {
            var existingDeliver = _deliverrepository.Get(id);
            _deliverrepository.UpdateItem(id, updateDeliver);

            return existingDeliver;
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "DeliverOnly")]
        public void Delete(int id)
        {
            var existingDeliver = _deliverrepository.Get(id);
            _deliverrepository.DeleteItem(id);
        }
    }
}
