using System.Collections.Generic;


namespace FitnessManager.Data
{
    public interface IRepository<T>
    {
        IEnumerable<T> Get();
        T Get(int id);
        T Add(T entity);
        void Update(T entity);
        void Delete(int entityId);
    }
}
