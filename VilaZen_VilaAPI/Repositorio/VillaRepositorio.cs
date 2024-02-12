using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VilaZen_VilaAPI.Data;
using VilaZen_VilaAPI.Models;
using VilaZen_VilaAPI.Repositorio.IRepositorio;

namespace VilaZen_VilaAPI.Repositorio
{
    public class VillaRepositorio : Repositoty<Villa>, IVillaRepositorio
    {
        private readonly ApplicationDataContext _db;

        public VillaRepositorio(ApplicationDataContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Villa> AtualizarAsync(Villa entity)
        {
            entity.DataAtualizacao = DateTime.Now;
            _db.Villas.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
