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
    public class CategoryController : ControllerBase
    {
        private readonly IService<CategoryDto> _categoryService;

        // public static string _directory = Environment.CurrentDirectory + "/images/";
        public CategoryController(IService<CategoryDto> categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public List<CategoryDto> Get()
        {
            return _categoryService.getAll();
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public CategoryDto Get(int id)
        {
            return _categoryService.getById(id);

        }

        // POST api/<CategoryController>
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public CategoryDto Post([FromForm] CategoryDto value)
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
            return _categoryService.addItem(value);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Put(int id, [FromBody] CategoryDto value)
        {
            if (value == null)
            {
                return BadRequest("Invalid data.");
            }
            var existingCategory = _categoryService.getById(id);
            if (existingCategory == null)
            {
                return NotFound("Category not found.");
            }
            // עדכון פרטי הקטגוריה
            var updatedCategory = _categoryService.Update(id, value);

            return Ok(updatedCategory);  // מחזיר את הקטגוריה המעודכנת
        }
        [HttpGet("getImage/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult getImage(int id)
        {
            CategoryDto c = _categoryService.getById(id);
            return File(c.Image, "image/jpeg");
        }
        //DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Delete(int id)
        {
            var existingCategory = _categoryService.getById(id);
            if (existingCategory == null)
            {
                return NotFound("Category not found.");
            }

            // מחיקת הקטגוריה
            _categoryService.Delete(id);

            return NoContent();  // מחזיר תשובה ללא תוכן (הקטגוריה נמחקה)
        }
    }
}
