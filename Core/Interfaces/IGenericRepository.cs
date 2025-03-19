using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllListAsync();
        Task<bool> SaveChangesAsync();
        void Delete(T entity);
        void Add(T entity);
        void Update(T entity);
        bool Exists(int id);

        //Specification endpoints
        Task<T?> GetEntityWithSpecAync(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GetListAsync(ISpecification<T> spec);
        Task<TResult?> GetEntityWithSpecAync<TResult>(ISpecification<T,TResult> spec);
        Task<IReadOnlyList<TResult>> GetListAsync<TResult>(ISpecification<T,TResult> spec);
        Task<int> CountAsync(ISpecification<T> spec);
    }
}
