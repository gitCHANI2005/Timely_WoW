using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class OwnerRepository : IRepository<Owner>
    {
        private readonly IContext context;
        public OwnerRepository(IContext context)
        {
            this.context = context;
        }

        public Owner AddItem(Owner item)
        {
            context.Owners.Add(item);
            context.save();
            return item;
        }

        public void DeleteItem(int id)
        {
            context.Owners.Remove(Get(id));
            context.save();
        }

        public Owner Get(int id)
        {
            return context.Owners.FirstOrDefault(x => x.Id == id);
        }

        public List<Owner> GetAll()
        {
            return context.Owners.ToList();
        }

        public int GetIdByName(string name)
        {
            throw new NotImplementedException();
        }

        public Owner UpdateItem(int id, Owner item)
        {
            Owner owner = Get(id);
            owner.Name = item.Name;
            owner.Phone = item.Phone; 
            owner.Identity = item.Identity;
            owner.Email = item.Email;
            owner.stores=item.stores;
            context.save();
            return owner;
        }
    }
}
