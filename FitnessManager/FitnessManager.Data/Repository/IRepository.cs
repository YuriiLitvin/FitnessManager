using System;
using System.Collections.Generic;
using System.Text;

namespace EF_HomeWork_4_CORE
{
    public interface IRepository<T>
    {
        IEnumerable<T> Get();
        T Add(T entity);
        void Update(T entity);
        void Delete(int entityId);
    }
}
