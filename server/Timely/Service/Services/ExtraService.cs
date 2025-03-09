using Service.Dtos;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ExtraService : IService<ExtraDto>
    {
        public ExtraDto addItem(ExtraDto item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<ExtraDto> getAll()
        {
            throw new NotImplementedException();
        }

        public ExtraDto getById(int id)
        {
            throw new NotImplementedException();
        }

        public ExtraDto Update(int id, ExtraDto item)
        {
            throw new NotImplementedException();
        }
    }
}
