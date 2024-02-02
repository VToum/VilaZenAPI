using Microsoft.EntityFrameworkCore;
using VilaZen_VilaAPI.Models;

namespace VilaZen_VilaAPI.Data
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options) 
        {
        
        }
        public DbSet<Villa> Villas { get; set; }
    }
}
