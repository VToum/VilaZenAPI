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

        public VilaZenController(ApplicationDataContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VillaDto>> BuscarVillas() 
        {
            return Ok(_db.Villas.ToList());

        }

        [HttpGet("{id:int}")]
        #region ResponseType
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        public ActionResult<VillaDto> BuscarVillaId(int id) 
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);

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
            if (_db.Villas.FirstOrDefault(u => u.Nome.ToLower() == villaDto.Nome.ToLower()) != null)
            {
                ModelState.AddModelError("CustomErro", "Nome da villa já existe");
                return BadRequest(ModelState);
            }

            if (villaDto == null)
            {
                return BadRequest(villaDto);
            }

            if (villaDto.Id > 0)
            {
                return BadRequest();
            }
            Villa model = new()
            {
                Id = villaDto.Id,
                Nome = villaDto.Nome,
                Detalhes = villaDto.Detalhes,
                Avaliar = villaDto.Avaliar,
                Ocupacao = villaDto.Ocupacao,
                ImageUrl = villaDto.ImageUrl,
                Cortesia = villaDto.Cortesia,
                Sqft = villaDto.Sqft
            };

            _db.Villas.Add(model);
            _db.SaveChanges();

            return CreatedAtRoute("BuscaVilla", new { id = villaDto.Id }, villaDto);  

        }

        [HttpDelete("{id:int}")]
        #region ResponseType
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        public IActionResult DeletaVilla(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            _db.Remove(villa);
            _db.SaveChanges();

            return NoContent();

        }

        [HttpPut("{id:int}")]
        public IActionResult AtualizaVilla(int id, [FromBody]VillaDto villaDto) 
        {
            if (villaDto == null || id != villaDto.Id == null)
            {
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(v => v.Id == id);
            if (villa.Id == null)
            {
                return BadRequest();
            }
            Villa model = new()
            {
                Nome = villaDto.Nome,
                Detalhes = villaDto.Detalhes,
                Avaliar = villaDto.Avaliar,
                Ocupacao = villaDto.Ocupacao,
                ImageUrl = villaDto.ImageUrl,
                Cortesia = villaDto.Cortesia,
                Sqft = villaDto.Sqft
            };
            _db.Villas.Update(model);
            _db.SaveChanges();

            return NoContent();

        }

        [HttpPatch("{id:int}")]
        public IActionResult AtualizaParcial(int id, JsonPatchDocument<VillaDto> atualizaVilla) 
        {
            if (atualizaVilla == null || id == 0)
            {
                return BadRequest();
            }
            var villa = _db.Villas.AsNoTracking().FirstOrDefault(u => u.Id == id);

            VillaDto villaDto = new ()
            {
                Id = villa.Id,
                Nome = villa.Nome,
                Detalhes = villa.Detalhes,
                Avaliar = villa.Avaliar,
                ImageUrl = villa.ImageUrl,
                Ocupacao = villa.Ocupacao,
                Cortesia = villa.Cortesia,
                Sqft = villa.Sqft
            };

            if (villa == null)
            {
                return BadRequest();
            }

            atualizaVilla.ApplyTo(villaDto, ModelState);

            Villa model = new()
            {
                Id = villaDto.Id,
                Nome = villaDto.Nome,
                Detalhes = villaDto.Detalhes,
                Avaliar = villaDto.Avaliar,
                Ocupacao = villaDto.Ocupacao,
                ImageUrl = villaDto.ImageUrl,
                Cortesia = villaDto.Cortesia,
                Sqft = villaDto.Sqft
            };

            _db.Villas.Update(model);
            _db.SaveChanges();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
