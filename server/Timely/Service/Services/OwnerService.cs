using AutoMapper;
using Repository.Entity;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Dtos;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class OwnerService : IService<OwnerDto>
    {
        private readonly IRepository<Owner> _ownerRepository;
        private readonly IMapper _mapper;
        public OwnerService(IRepository<Owner> ownerRepository, IMapper map)
        {
            _ownerRepository = ownerRepository;
            _mapper = map;
        }
        public OwnerDto addItem(OwnerDto item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<OwnerDto> getAll()
        {
            throw new NotImplementedException();
        }

        public OwnerDto getById(int id)
        {
            throw new NotImplementedException();
        }

        public OwnerDto RegisterDeliver(DeliverDto item)
        {
            throw new NotImplementedException();
        }

        public Deliver RegisterDeliver(OwnerDto item)
        {
            throw new NotImplementedException();
        }

        public OwnerDto Update(int id, OwnerDto item)
        {
            throw new NotImplementedException();
        }

        public Owner RegisterOwner(OwnerDto owner)
        {
            Owner o = _ownerRepository.AddItem(_mapper.Map<OwnerDto, Owner>(owner));
            return o;
        }

        Deliver IService<OwnerDto>.RegisterDeliver(DeliverDto item)
        {
            throw new NotImplementedException();
        }

        public Customer RegisterCustomer(CustomerDto customer)
        {
            throw new NotImplementedException();
        }
    }
}
