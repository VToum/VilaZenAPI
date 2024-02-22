using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography;
using VilaZen_VilaAPI.Models;
using VilaZen_Web.Models.Dto;
using VilaZen_Web.Services;

namespace VilaZen_Web.Controllers
{
    public class VillaController : Controller
    {

        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public VillaController(IVillaService villaService, IMapper mapper)
        {
            _villaService = villaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexVilla()
        {
            List<VillaDto> lista = new List<VillaDto>();

            var resposta = await _villaService.BuscarTodosAsync<APIResponse>();

            if (resposta != null && resposta.IsSuccess)
            {
                lista = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(resposta.Result));
            }

            return View(lista);
        }

        public async Task<IActionResult> CriarVilla()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarVilla(VillaCreateDto model)
        {
            if (ModelState.IsValid)
            {
                var responsa = await _villaService.CriarAsync<APIResponse>(model);

                if (responsa != null && responsa.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVilla));
                }
            }

            return View(model);
        }


        public async Task<IActionResult> AtualizarVilla(int villaId) 
        {
            var villa = await _villaService.BuscarPorId<APIResponse>(villaId);

            if (villa != null && villa.IsSuccess)
            {
                VillaDto model = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(villa.Result));
                return View(_mapper.Map<VillaUpdateDto>(model)) ;
            }
            return NotFound();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtualizarVilla(VillaUpdateDto model) 
        {
            if (ModelState.IsValid)
            {
                var resposta = await _villaService.AtualizarAsync<APIResponse>(model);

                if (resposta != null && resposta.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVilla));
                }
            }
            
            return View(model);
        
        }
  

        public async Task<IActionResult> RemoverVilla(int villaId) 
        {
            if (villaId != null)
            {
                var villa = await _villaService.BuscarPorId<APIResponse>(villaId);

                if (villa != null && villa.IsSuccess)
                {
                    var model = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(villa.Result));

                    return View(model);
                   
                }
            }
        
            return NoContent();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverVilla(VillaDto model) 
        {

            var resposta = await _villaService.DeleteAsync<APIResponse>(model.Id);

            if (resposta != null && resposta.IsSuccess) 
            {
            
                return RedirectToAction(nameof(IndexVilla));
            }

            return View(model);
        
        }















































































    }
}
