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
    [Route("api/VilaZenNumberAPI")]
    [ApiController]
    public class VilaZenNumberControllerAPI : ControllerBase
    {

        private readonly IVillaNumberRepositorio _dbVilaNumber;
        private readonly IVillaRepositorio _dbVilla;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public VilaZenNumberControllerAPI(IVillaNumberRepositorio dbVilaNumber, IMapper mapper, IVillaRepositorio dbVlla)
        {
            _dbVilaNumber = dbVilaNumber;
            _mapper = mapper;
            this._response = new();
            _dbVilla = dbVlla;
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> BuscarVillasNumbers() 
        {
            try
            {

                IEnumerable<VillaNumber> villasNumbersList = await _dbVilaNumber.BuscaTodosAsync(includeProperties:"Villa");
                _response.Result = _mapper.Map<List<VillaNumberDto>>(villasNumbersList);
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
        public async Task<ActionResult<APIResponse>> BuscarVillaNumberId(int id) 
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var villaNumber = await _dbVilaNumber.BuscaPorIdAsync(u => u.VillaNo == id);

                if (villaNumber == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "VillaNumber invalido " };
                    return BadRequest(_response);
                }

                _response.Result = _mapper.Map<VillaNumberDto>(villaNumber);
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
        public async Task<ActionResult<APIResponse>> CriarNumberVilla([FromBody]VillaNumberCreateDto createNumberDto) 
        {
            if (await _dbVilaNumber.BuscaPorIdAsync(u => u.VillaNo == createNumberDto.VillaNo) != null)
            {
                _response.Result = _mapper.Map<VillaNumber>(createNumberDto);
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Villa number invalido" };

                return BadRequest(_response);
            }

            if (createNumberDto == null || createNumberDto.VillaNo == 0)
            {
                _response.Result = _mapper.Map<VillaNumber>(createNumberDto);
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;

                return BadRequest(createNumberDto);
            }

            VillaNumber villaNumber = _mapper.Map<VillaNumber>(createNumberDto);

            await _dbVilaNumber.CriaVillaAsync(villaNumber);
            await _dbVilaNumber.SaveAsync();
            
            _response.Result = _mapper.Map<VillaNumber>(villaNumber);
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
        public async Task<ActionResult<APIResponse>> DeletaVillaNumber(int id)
        {
            if (id == null)
            {
                return BadRequest("erro id null");
            }

            var villaNumber = await _dbVilaNumber.BuscaPorIdAsync(u => u.VillaNo == id);

            if (villaNumber == null)
            {
                return NotFound("villaNumber id null");
            }
            await _dbVilaNumber.RemoveAsync(villaNumber);
            await _dbVilaNumber.SaveAsync();
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;

            return Ok(_response);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<APIResponse>> AtualizaVillaNumber(int id, [FromBody]VillaNumberUpdateDto updateNumberDto) 
        {
            try
            {
                VillaNumber modelNumber = _mapper.Map<VillaNumber>(updateNumberDto);

                await _dbVilaNumber.AtualizarAsync(modelNumber);
                await _dbVilaNumber.SaveAsync();
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;

                return Ok(_response);
            }
            catch (Exception)
            {
                if (updateNumberDto == null || id != updateNumberDto.VillaNo)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
            }
            return NotFound(_response);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> AtualizaParcialNumber(int id, JsonPatchDocument<VillaNumberUpdateDto> atualizaVillaNumber) 
        {
            if (atualizaVillaNumber == null || id == 0)
            {
                return BadRequest("erro atualizaVillaNumber");
            }
            var villaNumber = await _dbVilaNumber.BuscaPorIdAsync(u => u.VillaNo == id, tracked:false);

            VillaNumberUpdateDto villaNumberDTO = _mapper.Map<VillaNumberUpdateDto>(villaNumber);

            if (villaNumber == null)
            {
                return BadRequest("erro villaNumber null");
            }

            atualizaVillaNumber.ApplyTo(villaNumberDTO, ModelState);

            VillaNumber modelNumber = _mapper.Map<VillaNumber>(villaNumberDTO);

            await _dbVilaNumber.AtualizarAsync(modelNumber);
            await _dbVilaNumber.SaveAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
