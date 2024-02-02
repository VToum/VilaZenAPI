using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using VilaZen_VilaAPI.Data;
using VilaZen_VilaAPI.Models;
using VilaZen_VilaAPI.Models.Dto;


namespace VilaZen_VilaAPI.Controllers
{
    [Route("api[controller]")]
    [ApiController]
    public class VilaZenController : ControllerBase
    {
        private readonly ILogger<VilaZenController> _logger;

        public VilaZenController(ILogger<VilaZenController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VillaDto>> BuscarVillas() 
        {
            _logger.LogInformation("Busca todas vilas");
            var villa = VillaStore.villaList;

            return Ok(villa);

        }

        [HttpGet("{id:int}")]
        #region ResponseType
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        public ActionResult<VillaDto> BuscarVillaId(int id) 
        {

            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);

            return Ok(villa);

        }

        [HttpPost]
        #region ResponseType
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        public ActionResult<VillaDto> CriarVilla([FromBody]VillaDto villaDto) 
        {
            if (VillaStore.villaList.FirstOrDefault(u => u.Nome.ToLower() == villaDto.Nome.ToLower())!=null)
            {
                ModelState.AddModelError("CustomErro", "Nome da villa já existe");
                return BadRequest(ModelState);
            }

            if (villaDto == null)
            {
                _logger.LogError("Erro ao buscar vida do id: ", villaDto.Id);
                return BadRequest(villaDto);
            }

            if (villaDto.Id > 0)
            {
                return BadRequest();
            }

            villaDto.Id = VillaStore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            VillaStore.villaList.Add(villaDto); 

            return Ok(villaDto);    

        }

        [HttpDelete("{id:int}")]
        #region ResponseType
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        public IActionResult DeletaVilla(int id)
        {
            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            VillaStore.villaList.Remove(villa);

            return NoContent();

        }

        [HttpPut("{id:int}")]
        public IActionResult AtualizaVilla(int id, [FromBody]VillaDto villaDto) 
        {
            if (villaDto == null || id != villaDto.Id == null)
            {
                return BadRequest();
            }

            var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);

            if (villa.Id == null)
            {
                return BadRequest();
            }

            villa.Nome = villaDto.Nome;
            villa.Ocupacao = villaDto.Ocupacao;

            return NoContent();

        }

        [HttpPatch("{id:int}")]
        public IActionResult AtualizaParcial(int id, JsonPatchDocument<VillaDto> atualizaVilla) 
        {
            if (atualizaVilla == null || id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);

            if (villa == null)
            {
                return BadRequest();
            }
            atualizaVilla.ApplyTo(villa, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
