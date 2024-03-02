﻿using VilaZen_Utility;
using VilaZen_Web.Models;
using VilaZen_Web.Models.Dto;

namespace VilaZen_Web.Services
{
    public class VillaNumberService : BaseService, IVillaNumberService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string VillaUrl;
        public VillaNumberService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            VillaUrl = configuration.GetValue<string>("ServiceUrls:VilaZenAPI");

        }

        public Task<T> AtualizarAsync<T>(VillaNumberUpdateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.Put,
                Data = dto,
                Url = VillaUrl + "/api/VilaZenAPI/" + dto.VillaNo

            });
        }

        public Task<T> BuscarPorId<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.Get,
                Url = VillaUrl + "/api/VilaZenAPI/" + id
            });
        }

        public Task<T> BuscarTodosAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.Get,
                Url = VillaUrl + "/api/VilaZenAPI/"
            });
            

        }

        public Task<T> CriarAsync<T>(VillaNumberCreateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType= SD.ApiType.Post,
                Data= dto,
                Url= VillaUrl + "/api/VilaZenAPI/"

            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.Delete,
                Url = VillaUrl + "/api/VilaZenAPI/" + id
            });
        }
    }
}
