using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    internal class StoreRepository : IRepository<Store>
    {
        private readonly IContext context;
        public StoreRepository(IContext context)
        {
            this.context = context;
        }
        public Store AddItem(Store item)
        {
            context.Stores.Add(item);
            context.save();
            return item;
        }

        public void DeleteItem(int id)
        {
            context.Stores.Remove(Get(id));
            context.save();
        }

        public Store Get(int id)
        {
            return context.Stores.FirstOrDefault(x => x.Id == id);
        }

        public List<Store> GetAll()
        {
            return context.Stores.ToList();
            
        }

        public Store UpdateItem(int id, Store item)
        {
            Store store = Get(id);
            store.Name=item.Name;
            store.IdOwner=item.IdOwner;
            store.owner = item.owner;
            store.IdCity=item.IdCity;
            store.city=item.city;
            store.Address=item.Address;
            store.IsManager=item.IsManager;
            store.IsOwner=item.IsOwner;
            store.ImgUrl=item.ImgUrl;
            store.ListExtra=item.ListExtra;
            store.WeekOpen=item.WeekOpen;
            store.WeekClose=item.WeekClose;
            store.FridayOpen=item.FridayOpen;   
            store.FridayClose=item.FridayClose;
            store.ShabbatOpen=item.ShabbatOpen;
            store.ShabbatClose=item.ShabbatClose;
            context.save();
            return store;
        }

    }
}
