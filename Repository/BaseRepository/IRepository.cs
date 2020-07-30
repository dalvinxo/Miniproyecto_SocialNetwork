using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.BaseRepository
{
    interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> Update(T entity);
        Task<T> UpdateExplicito(T entity);

        Task<T> Delete(int id);
        Task<T> DeleteEntity(T entiry);
    }
}
