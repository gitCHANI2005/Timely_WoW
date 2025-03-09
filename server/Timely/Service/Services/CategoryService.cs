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
    public class CategoryService : IService<CategoryDto>
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public CategoryDto addItem(CategoryDto item)
        {
            return _mapper.Map<CategoryDto>(_repository.AddItem(_mapper.Map<Category>(item)));
        }

        public void Delete(int id)
        {
            _repository.DeleteItem(id);

        }

        public List<CategoryDto> getAll()
        {
            return _mapper.Map<List<CategoryDto>>(_repository.GetAll());

        }

        public CategoryDto getById(int id)
        {
            return _mapper.Map<CategoryDto>(_repository.Get(id));

        }

        public CategoryDto Update(int id, CategoryDto item)
        {
            return _mapper.Map<CategoryDto>(_repository.UpdateItem(id, _mapper.Map<Category>(item)));

        }

    }

}
    

