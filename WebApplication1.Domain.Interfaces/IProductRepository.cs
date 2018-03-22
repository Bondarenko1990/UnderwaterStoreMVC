using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Domain.Core;

namespace WebApplication1.Domain.Interfaces
{
    public interface IProductRepository<T>  where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void SaveItem(T item);
        void Create(T item);
        void Update(T item);
        T Delete(int Id);
    }
}
