using AutoMapper;
using Repository.Entity;
using Repository.Interfaces;
using Service.Dtos;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class StoreService : IService<StoreDto>
    {
        private readonly IRepository<Store> _repository;
        private readonly IMapper _mapper;

        public StoreService(IRepository<Store> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public StoreDto addItem(StoreDto item)
        {
            return _mapper.Map<StoreDto>(_repository.AddItem(_mapper.Map<Store>(item)));
        }
        
        public void Delete(int id)
        {
            _repository.DeleteItem(id);
        }

        public List<StoreDto> getAll()
        {
            return _mapper.Map<List<StoreDto>>(_repository.GetAll());
        }

        public StoreDto getById(int id)
        {
            return _mapper.Map<StoreDto>(_repository.Get(id));
        }

        public Customer RegisterCustomer(CustomerDto customer)
        {
            throw new NotImplementedException();
        }

        public StoreDto RegisterDeliver(DeliverDto item)
        {
            throw new NotImplementedException();
        }

        public Deliver RegisterDeliver(StoreDto item)
        {
            throw new NotImplementedException();
        }

        public Owner RegisterOwner(OwnerDto owner)
        {
            throw new NotImplementedException();
        }

        public StoreDto Update(int id, StoreDto item)
        {
            return _mapper.Map<StoreDto>(_repository.UpdateItem(id, _mapper.Map<Store>(item)));

        }

        Deliver IService<StoreDto>.RegisterDeliver(DeliverDto item)
        {
            throw new NotImplementedException();
        }
    }
}
