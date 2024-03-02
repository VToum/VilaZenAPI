using VilaZen_Web.Models.Dto;

namespace VilaZen_Web.Services
{
    public interface IVillaNumberService
    {
        Task<T> CriarAsync<T>(VillaNumberCreateDto dto);
        Task<T> AtualizarAsync<T>(VillaNumberUpdateDto dto);
        Task<T> BuscarTodosAsync<T>();
        Task<T> BuscarPorId<T>(int id);
        Task<T> DeleteAsync<T>(int id); 
    }
}
