using System;
using System.Collections.Generic;
using System.Text;

namespace Race.Repo.Interfaces
{
    interface IRepostitory<T> where T : class
    {
        T Create(T entity);
        IEnumerable<T> GetAll();
        T Get(int id);
        int Update(T entity);
        void Delete(int id);
    }
}
