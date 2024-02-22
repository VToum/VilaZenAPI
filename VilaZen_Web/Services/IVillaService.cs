using VilaZen_Web.Models.Dto;

namespace VilaZen_Web.Services
{
    public interface IVillaService
    {
        Task<T> CriarAsync<T>(VillaCreateDto dto);
        Task<T> AtualizarAsync<T>(VillaUpdateDto dto);
        Task<T> BuscarTodosAsync<T>();
        Task<T> BuscarPorId<T>(int id);
        Task<T> DeleteAsync<T>(int id); 


    }
}
