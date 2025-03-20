using AutoMapper;
using Repository.Entity;
using Service.Dtos;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CityService : IService<CityDto>
    {
        public CityDto addItem(CityDto item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<CityDto> getAll()
        {
            throw new NotImplementedException();
        }

        public CityDto getById(int id)
        {
            throw new NotImplementedException();
        }

        public Customer RegisterCustomer(CustomerDto customer)
        {
            throw new NotImplementedException();
        }

        public CityDto RegisterDeliver(DeliverDto item)
        {
            throw new NotImplementedException();
        }

        public Deliver RegisterDeliver(CityDto item)
        {
            throw new NotImplementedException();
        }

        public Owner RegisterOwner(OwnerDto owner)
        {
            throw new NotImplementedException();
        }

        public CityDto Update(int id, CityDto item)
        {
            throw new NotImplementedException();
        }

        Deliver IService<CityDto>.RegisterDeliver(DeliverDto item)
        {
            throw new NotImplementedException();
        }
    }
}
