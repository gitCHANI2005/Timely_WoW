using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class DeliverRepository : IRepository<Deliver>, IActiveDelivers<Deliver>
    {
        private readonly IContext context;
        public DeliverRepository(IContext context)
        {
            this.context = context;
        }
        public Deliver AddItem(Deliver item)
        {
            context.Delivers.Add(item);
            context.save();
            return item;
        }

        public void DeleteItem(int id)
        {
            context.Delivers.Remove(Get(id));
            context.save();
        }

        public Deliver Get(int id)
        {
            return context.Delivers.FirstOrDefault(x => x.Id == id);
        }

        public List<Deliver> GetAll()
        {
            return context.Delivers.ToList();
        }

        public List<Deliver> GetAllActiveDelivers()
        {
            return context.Delivers.Where(d => d.IsActive).ToList();
        }

        public Deliver UpdateItem(int id, Deliver item)
        {
            Deliver deliver = Get(id);
            deliver.Name = item.Name;
            deliver.Identity = item.Identity;
            deliver.CityId=item.CityId;
            deliver.IsActive=item.IsActive;
            deliver.BankAccount=item.BankAccount;
            deliver.BankBranch=item.BankBranch;
            deliver.NumOfCar=deliver.NumOfCar;
            deliver.DateOfBirth=item.DateOfBirth;
            deliver.BankNumber=item.BankNumber;
            deliver.city=item.city;
            deliver.Phone=item.Phone;
            deliver.Email=item.Email;
            context.save();
            return deliver;
        }
    }
}
