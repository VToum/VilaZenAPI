using VilaZen_Web.Models;

namespace VilaZen_Web.Services
{
    public interface IBaseService
    {
        APIRequest responseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiResponse);
    }
}
