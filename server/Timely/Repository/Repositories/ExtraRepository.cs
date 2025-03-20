using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ExtraRepository : IRepository<Extra>
    {
        private readonly IContext context;
        public ExtraRepository(IContext context)
        {
            this.context = context;
        }
        public Extra AddItem(Extra item)
        {
            context.Extras.Add(item);
            context.save();
            return item;
        }

        public void DeleteItem(int id)
        {
            context.Extras.Remove(Get(id));
            context.save();
        }

        public Extra Get(int id)
        {
            return context.Extras.FirstOrDefault(x => x.Id == id);
        }

        public List<Extra> GetAll()
        {
            return context.Extras.ToList();
        }

        public int GetIdByName(string name)
        {
            throw new NotImplementedException();
        }

        public Extra UpdateItem(int id, Extra item)
        {
            Extra extra = Get(id);
            extra.cost = item.cost;
            extra.Name = item.Name;
            context.save();
            return extra;
        }
    }
}
