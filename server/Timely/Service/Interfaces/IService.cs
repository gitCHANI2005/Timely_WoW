using Repository.Entity;
using Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IService<T>
    {
        List<T> getAll();
        T getById(int id);
        T addItem(T item);
        T Update(int id, T item);
        void Delete(int id);
    }
}
