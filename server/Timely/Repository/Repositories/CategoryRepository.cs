
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly IContext context;
        public CategoryRepository(IContext context)
        {
            this.context = context;
        }

        public Category AddItem(Category item)
        {
            context.Categories.Add(item);
            Console.WriteLine($"Saving Category: {item.Name}, {item.ImageUrl}");
            context.save();
            return item;
        }

        public void DeleteItem(int id)
        {
            context.Categories.Remove(Get(id));
            context.save();
        }

        public Category Get(int id)
        {
            return context.Categories.FirstOrDefault(x => x.Id == id);
        }

        public List<Category> GetAll()
        {
            return context.Categories.ToList();
        }

        public int GetIdByName(string name)
        {
            throw new NotImplementedException();
        }

        public Category UpdateItem(int id, Category item)
        {
            Category category = Get(id);
            category.Name = item.Name;
            category.ImageUrl = item.ImageUrl;
            context.save();
            return category;

        }
    }
}