using Microsoft.AspNetCore.Mvc;
using Repository.Entity;
using Repository.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Timely.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtraController : ControllerBase
    {
        private readonly IRepository<Extra> _extrarepository;
        public ExtraController(IRepository<Extra> extrarepository)
        {
            _extrarepository = extrarepository;
        }
        // GET: api/<ExtraController>
        [HttpGet]
        public List<Extra> Get()
        {
            return _extrarepository.GetAll();
        }

        // GET api/<ExtraController>/5
        [HttpGet("{id}")]
        public Extra Get(int id)
        {
            return _extrarepository.Get(id);
        }

        // POST api/<ExtraController>
        [HttpPost]
        public ActionResult<Extra> Post([FromBody] Extra newExtra)
        {
            if (newExtra == null)
            {
                return BadRequest("Customer cannot be null");
            }

            try
            {
                var createdExtra = _extrarepository.AddItem(newExtra);
                return CreatedAtAction(nameof(Get), new { id = createdExtra.Id }, createdExtra);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT api/<ExtraController>/5
        [HttpPut("{id}")]
        public Extra Put(int id, [FromBody] Extra updateExtra)
        {
            var existingDeliver = _extrarepository.Get(id);
            _extrarepository.UpdateItem(id, updateExtra);
            return existingDeliver;
        }

        // DELETE api/<ExtraController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var existingExtra = _extrarepository.Get(id);
            _extrarepository.DeleteItem(id);
        }
    }
}
