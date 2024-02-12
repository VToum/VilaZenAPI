using System.Linq.Expressions;
using VilaZen_VilaAPI.Models;

namespace VilaZen_VilaAPI.Repositorio.IRepositorio
{
    public interface IVillaNumberRepositorio : IRepository<VillaNumber>
    {
        Task<VillaNumber> AtualizarAsync(VillaNumber entity);  
    }
}
