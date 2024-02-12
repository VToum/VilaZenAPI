using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VilaZen_VilaAPI.Data;
using VilaZen_VilaAPI.Models;
using VilaZen_VilaAPI.Repositorio.IRepositorio;

namespace VilaZen_VilaAPI.Repositorio
{
    public class VillaNumberRepositorio : Repositoty<VillaNumber>, IVillaNumberRepositorio
    {
        private readonly ApplicationDataContext _db;

        public VillaNumberRepositorio(ApplicationDataContext db) : base(db)
        {
            _db = db;
        }
        public async Task<VillaNumber> AtualizarAsync(VillaNumber entity)
        {
            entity.AtualizaData = DateTime.Now;
            _db.VillaNumbers.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
