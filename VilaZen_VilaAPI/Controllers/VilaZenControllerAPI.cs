using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using VilaZen_VilaAPI.Data;
using VilaZen_VilaAPI.Models;
using VilaZen_VilaAPI.Models.Dto;
using VilaZen_VilaAPI.Repositorio.IRepositorio;


namespace VilaZen_VilaAPI.Controllers
{
    [Route("api/VilaZenAPI")]
    [ApiController]
    public class VilaZenControllerAPI : ControllerBase
    {

        private readonly IVillaRepositorio _dbVila;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public VilaZenControllerAPI(IVillaRepositorio dbVila, IMapper mapper)
        {
            _dbVila = dbVila;
            _mapper = mapper;
            this._response = new();
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>>BuscarVillas() 
        {
            try
            {

                IEnumerable<Villa> villasList = await _dbVila.BuscaTodosAsync();
                _response.Result = _mapper.Map<List<VillaDto>>(villasList);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return Ok(_response);
          

        }

        [HttpGet("{id:int}")]
        #region ResponseType
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        public async Task<ActionResult<APIResponse>> BuscarVillaId(int id) 
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            try
            {
                var villa = await _dbVila.BuscaPorIdAsync(u => u.Id == id);

                _response.Result = _mapper.Map<VillaDto>(villa);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
        }

        [HttpPost]
        #region ResponseType
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        public async Task<ActionResult<APIResponse>> CriarVilla([FromBody]VillaCreateDto createDto) 
        {
            if (await _dbVila.BuscaPorIdAsync(u => u.Nome.ToLower() == createDto.Nome.ToLower()) != null)
            {
                ModelState.AddModelError("CustomErro", "Nome da villa já existe");
                
                return BadRequest(ModelState);
            }

            if (createDto == null)
            {
                return BadRequest(createDto);
            }

            if (createDto.Id > 0)
            {
                return BadRequest();
            }
            Villa villa = _mapper.Map<Villa>(createDto);

            await _dbVila.CriaVillaAsync(villa);
            await _dbVila.SaveAsync();
            
            _response.Result = _mapper.Map<Villa>(villa);
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;


            return Created("Criado com sucesso", _response);  

        }

        [HttpDelete("{id:int}")]
        #region ResponseType
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        public async Task<ActionResult<APIResponse>> DeletaVilla(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var villa = await _dbVila.BuscaPorIdAsync(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            await _dbVila.RemoveAsync(villa);
            await _dbVila.SaveAsync();
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;

            return Ok(_response);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<APIResponse>> AtualizaVilla(int id, [FromBody]VillaUpdateDto updateDto) 
        {
            try
            {
                Villa model = _mapper.Map<Villa>(updateDto);

                await _dbVila.AtualizarAsync(model);
                await _dbVila.SaveAsync();
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;

                return Ok(_response);
            }
            catch (Exception)
            {
                if (updateDto == null || id != updateDto.Id)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
            }
            return NotFound(_response);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> AtualizaParcial(int id, JsonPatchDocument<VillaUpdateDto> atualizaVilla) 
        {
            if (atualizaVilla == null || id == 0)
            {
                return BadRequest();
            }
            var villa = await _dbVila.BuscaPorIdAsync(u => u.Id == id, tracked:false);

            VillaUpdateDto villaDTO = _mapper.Map<VillaUpdateDto>(villa);

            if (villa == null)
            {
                return BadRequest();
            }

            atualizaVilla.ApplyTo(villaDTO, ModelState);

            Villa model = _mapper.Map<Villa>(villaDTO);

            await _dbVila.AtualizarAsync(model);
            await _dbVila.SaveAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
