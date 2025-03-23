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
    public class MenuDoseService : IService<MenuDoseDto>
    {
        private readonly IRepository<MenuDose> _repository;
        private readonly IMapper _mapper;

        public MenuDoseService(IRepository<MenuDose> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public MenuDoseDto addItem(MenuDoseDto item)
        {
            return _mapper.Map<MenuDoseDto>(_repository.AddItem(_mapper.Map<MenuDose>(item)));

        }

        public void Delete(int id)
        {
            _repository.DeleteItem(id);

        }

        public List<MenuDoseDto> getAll()
        {
            return _mapper.Map<List<MenuDoseDto>>(_repository.GetAll());
        }

        public MenuDoseDto getById(int id)
        {
            return _mapper.Map<MenuDoseDto>(_repository.Get(id));
        }

        public MenuDoseDto Update(int id, MenuDoseDto item)
        {
            return _mapper.Map<MenuDoseDto>(_repository.UpdateItem(id, _mapper.Map<MenuDose>(item)));

        }
    }
}
