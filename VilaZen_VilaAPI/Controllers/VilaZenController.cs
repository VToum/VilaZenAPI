using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly ApplicationDataContext _db;
        private readonly IMapper _mapper;

        public VilaZenController(ApplicationDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VillaDto>>> BuscarVillas() 
        {
            IEnumerable<Villa> villasList = await _db.Villas.ToListAsync();

            return Ok(_mapper.Map<List<VillaDto>>(villasList));

        }

        [HttpGet("{id:int}")]
        #region ResponseType
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        public async Task<ActionResult<VillaDto>> BuscarVillaId(int id) 
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);

            return Ok(_mapper.Map<VillaDto>(villa));

        }

        [HttpPost]
        #region ResponseType
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        public async Task<ActionResult<VillaDto>> CriarVilla([FromBody]VillaCreateDto createDto) 
        {
            if (await _db.Villas.FirstOrDefaultAsync(u => u.Nome.ToLower() == createDto.Nome.ToLower()) != null)
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
            Villa model = _mapper.Map<Villa>(createDto);

            await _db.Villas.AddAsync(model);
            await _db.SaveChangesAsync();

            return Created("Criado com sucesso", model);  

        }

        [HttpDelete("{id:int}")]
        #region ResponseType
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        public async Task<IActionResult> DeletaVilla(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var villa = await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            _db.Remove(villa);
            await _db.SaveChangesAsync();

            return NoContent();

        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> AtualizaVilla(int id, [FromBody]VillaUpdateDto updateDto) 
        {
            if (updateDto == null || id != updateDto.Id)
            {
                return BadRequest();
            }

            Villa model = _mapper.Map<Villa>(updateDto);

            _db.Villas.Update(model);
            await _db.SaveChangesAsync();

            return NoContent();

        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> AtualizaParcial(int id, JsonPatchDocument<VillaUpdateDto> atualizaVilla) 
        {
            if (atualizaVilla == null || id == 0)
            {
                return BadRequest();
            }
            var villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

            VillaUpdateDto villaDTO = _mapper.Map<VillaUpdateDto>(atualizaVilla);

            if (villa == null)
            {
                return BadRequest();
            }

            atualizaVilla.ApplyTo(villaDTO, ModelState);

            Villa model = _mapper.Map<Villa>(villaDTO);

            _db.Villas.Update(model);
            await _db.SaveChangesAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
