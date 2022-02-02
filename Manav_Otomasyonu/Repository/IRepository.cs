using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manav_Otomasyonu.Repository
{
    public interface IRepository<T>
    {
            List<T> Get();
            T GetById(int id);
            int Create(T item);
            int Update(T item);
            void Delete(int id);
    }
}
