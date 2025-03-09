using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Dtos;
using Service.Interfaces;
using Service.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Timely.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuDoseController : ControllerBase
    {
        private readonly IService<MenuDoseDto> _menuDoseService;
        //public static string _directory = Environment.CurrentDirectory + "/images/";
        public MenuDoseController(IService<MenuDoseDto> menuDoseService)
        {
            _menuDoseService = menuDoseService;
        }
        // GET: api/<MenuDoseController>
        [HttpGet]
        public List<MenuDoseDto> Get()
        {
            return _menuDoseService.getAll();
        }

        // GET api/<MenuDoseController>/5
        [HttpGet("{id}")]
        public MenuDoseDto Get(int id)
        {
            return _menuDoseService.getById(id);
        }

        // POST api/<MenuDoseController>
        [HttpPost]
        [Authorize(Policy = "ManagerOnly")]
        public MenuDoseDto Post([FromForm] MenuDoseDto value)
        {

            if (value.File != null)
            {
                var imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Images");
                // בדוק אם התיקייה קיימת, ואם לא – צור אותה
                if (!Directory.Exists(imageDirectory))
                {
                    Directory.CreateDirectory(imageDirectory);
                }

                // צור את הנתיב המלא של הקובץ
                var filePath = Path.Combine(imageDirectory, value.File.FileName);
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    value.File.CopyTo(fs);
                }
                value.Image = System.IO.File.ReadAllBytes(filePath);
            }
            return _menuDoseService.addItem(value);
        }
        [HttpGet("getImage/{id}")]
        [Authorize(Policy = "ManagerOnly")]
        public IActionResult getImage(int id)
        {
            MenuDoseDto c = _menuDoseService.getById(id);
            return File(c.Image, "image/jpeg");
        }

        // PUT api/<MenuDoseController>/5
        [HttpPut("{id}")]
        [Authorize(Policy = "ManagerOnly")]
        public IActionResult Put(int id, [FromBody] MenuDoseDto value)
        {
            if (value == null)
            {
                return BadRequest("Invalid data.");
            }
            var existingMenuDose = _menuDoseService.getById(id);
            if (existingMenuDose == null)
            {
                return NotFound("MenuDose not found.");
            }
            // עדכון פרטי הקטגוריה
            var updatedMenuDose = _menuDoseService.Update(id, value);

            return Ok(updatedMenuDose);  // מחזיר את הקטגוריה המעודכנת
        }

        // DELETE api/<MenuDoseController>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "ManagerOnly")]
        public IActionResult Delete(int id)
        {
            var existingMenuDose = _menuDoseService.getById(id);
            if (existingMenuDose == null)
            {
                return NotFound("MenuDose not found.");
            }

            // מחיקת הקטגוריה
            _menuDoseService.Delete(id);

            return NoContent();  // מחזיר תשובה ללא תוכן (הקטגוריה נמחקה)
        }
    }
}
