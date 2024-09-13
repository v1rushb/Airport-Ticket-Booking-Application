using System.Collections.Generic;

namespace Airport.Interfaces {
    public interface IRepository<T> {
        IEnumerable<T> GetAll();
        T? GetByID(string id);
        void Add(T val);
        void Update(T val);
        void Delete(string ID);
    }
}