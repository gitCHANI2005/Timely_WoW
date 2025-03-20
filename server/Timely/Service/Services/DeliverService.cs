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
    public class DeliverService : IService<DeliverDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Deliver> _deliverRepository;
        public DeliverService(IMapper map, IRepository<Deliver> deliverRepository)
        {
            _mapper = map;
            _deliverRepository = deliverRepository;
        }
        public DeliverDto addItem(DeliverDto item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<DeliverDto> getAll()
        {
            throw new NotImplementedException();
        }

        public DeliverDto getById(int id)
        {
            throw new NotImplementedException();
        }

        public DeliverDto Update(int id, DeliverDto item)
        {
            throw new NotImplementedException();
        }

        public Deliver RegisterDeliver(DeliverDto deliver)
        {
            //string role = Roles.deliver + "";
            Deliver d = _deliverRepository.AddItem(_mapper.Map<DeliverDto, Deliver>(deliver));
            return d;
        }

        public Owner RegisterOwner(OwnerDto owner)
        {
            throw new NotImplementedException();
        }

        public Customer RegisterCustomer(CustomerDto customer)
        {
            throw new NotImplementedException();
        }
    }
}
