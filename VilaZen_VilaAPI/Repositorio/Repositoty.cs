using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.X86;
using VilaZen_VilaAPI.Data;
using VilaZen_VilaAPI.Models;
using VilaZen_VilaAPI.Repositorio.IRepositorio;

namespace VilaZen_VilaAPI.Repositorio
{
    public class Repositoty<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDataContext _db;
        internal DbSet<T> _dbSet;
        public Repositoty(ApplicationDataContext db)
        {
            _db = db;
            //SS_db.VillaNumbers.Include(u => u.Villa).ToList();
            this._dbSet = _db.Set<T>();
        }
        public async Task<List<T>> BuscaTodosAsync(Expression<Func<T, bool>>? filtrar = null, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (filtrar != null)
            {
                query = query.Where(filtrar);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<T> BuscaPorIdAsync(Expression<Func<T, bool>> filtrar = null, bool tracked = true, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;

            if (query != null)
            {
                query = query.Where(filtrar);
            }
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task CriaVillaAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
