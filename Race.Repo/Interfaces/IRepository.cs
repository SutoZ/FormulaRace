using Race.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Race.Repo.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<int> InsertAsync(T entity);
        IEnumerable<T> GetAllPilot();
        Task<T> GetAsync(int id);
        Task UpdateAsync();
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
