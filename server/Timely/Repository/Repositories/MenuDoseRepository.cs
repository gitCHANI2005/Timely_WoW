using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class MenuDoseRepository : IRepository<MenuDose>
    {
        private readonly IContext context;
        public MenuDoseRepository(IContext context)
        {
            this.context = context;
        }

        public MenuDose AddItem(MenuDose item)
        {
            context.MenuDoses.Add(item);
            context.save();
            return item;
        }

        public void DeleteItem(int id)
        {
            context.MenuDoses.Remove(Get(id));
            context.save();
        }

        public MenuDose Get(int id)
        {
            return context.MenuDoses.FirstOrDefault(x => x.Id == id);
        }

        public List<MenuDose> GetAll()
        {
            return context.MenuDoses.ToList();
        }

        public int GetIdByName(string name)
        {
            throw new NotImplementedException();
        }

        public MenuDose UpdateItem(int id, MenuDose item)
        {
            MenuDose menuDose = Get(id);
            menuDose.cost = item.cost;
            menuDose.Description = item.Description;
            menuDose.countLikes = item.countLikes;
            menuDose.AvgLikes = item.AvgLikes;
            menuDose.store = item.store;
            menuDose.category = item.category;
            menuDose.Name = item.Name;
            menuDose.CategoryId = item.CategoryId;
            menuDose.ListExtra = item.ListExtra;
            menuDose.ImageUrl = item.ImageUrl;
            context.save();
            return menuDose;
        }
    }
}
