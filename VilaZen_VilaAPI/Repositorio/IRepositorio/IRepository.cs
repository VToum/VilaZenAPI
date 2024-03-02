using System.Linq.Expressions;
using VilaZen_VilaAPI.Models;

namespace VilaZen_VilaAPI.Repositorio.IRepositorio
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> BuscaTodosAsync(Expression<Func<T, bool>>? filtrar = null, string? includeProperties = null);
        Task<T> BuscaPorIdAsync(Expression<Func<T, bool>> filtrar = null, bool tracked = true, string? includeProperties = null);
        Task CriaVillaAsync(T entity);
        Task RemoveAsync(T entity);
        Task SaveAsync();

    }
}
