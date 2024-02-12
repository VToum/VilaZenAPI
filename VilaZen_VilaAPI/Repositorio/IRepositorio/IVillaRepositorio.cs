using System.Linq.Expressions;
using VilaZen_VilaAPI.Models;

namespace VilaZen_VilaAPI.Repositorio.IRepositorio
{
    public interface IVillaRepositorio : IRepository<Villa>
    {
        Task<Villa> AtualizarAsync(Villa entity);  
    }
}
