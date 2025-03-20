using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    internal class CityRepository : IRepository<City>
    {
        private readonly IContext context;
        public CityRepository(IContext context)
        {
            this.context = context;
        }
        public City AddItem(City item)
        {
            context.Cities.Add(item);
            context.save();
            return item;
        }

        public void DeleteItem(int id)
        {
            context.Cities.Remove(Get(id));
            context.save();
        }

        public City Get(int id)
        {
            return context.Cities.FirstOrDefault(x => x.Id == id);
        }

        public List<City> GetAll()
        {
            return context.Cities.ToList();
        }

        public City UpdateItem(int id, City item)
        {
            City city = Get(id);
            city.Name = item.Name;
            context.save();
            return city;
        }

        public int GetIdByName(string name)
        {
            return context.Cities
            .Where(x => x.Name == name)
            .Select(x => x.Id)
            .FirstOrDefault();
        }

    }
}
