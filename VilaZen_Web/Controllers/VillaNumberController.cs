using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VilaZen_Web.Models;
using VilaZen_Web.Models.Dto;
using VilaZen_Web.Services;

namespace VilaZen_Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberService _dbVilaNumberService;
        private readonly IMapper _mapper;

        public VillaNumberController(IVillaNumberService dbVilaNumberService, IMapper mapper)
        {
            _dbVilaNumberService = dbVilaNumberService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexVillaNumber() 
        {
            List<VillaNumberDto> list = new();

            var villas = await _dbVilaNumberService.BuscarTodosAsync<APIResponse>();

            if (villas != null && villas.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<List<VillaNumberDto>>(Convert.ToString(villas.Result));

            }
            return View(list);

        }

    }
}
