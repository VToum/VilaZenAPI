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
        public DbSet<VillaNumber> VillaNumbers { get; set; }

        #region BaseModelBuilder
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Villa>().HasData(new Villa {
        //        Id = 1,
        //        Nome = "Royal Villa",
        //        Detalhes = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
        //        ImageUrl = "https://dotnetmastery.com/bluevillaimages/villa3.jpg",
        //        Avaliar = 4,
        //        Ocupacao = 200,
        //        Sqft = 550,
        //        Cortesia = "",
        //        DataCriacao = DateTime.Now
        //    },
        //      new Villa
        //      {
        //          Id = 2,
        //          Nome = "Premium Pool Villa",
        //          Detalhes = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
        //          ImageUrl = "https://dotnetmastery.com/bluevillaimages/villa1.jpg",
        //          Avaliar = 4,
        //          Ocupacao = 300,
        //          Sqft = 550,
        //          Cortesia = "",
        //          DataCriacao = DateTime.Now
        //      },
        //      new Villa
        //      {
        //          Id = 3,
        //          Nome = "Luxury Pool Villa",
        //          Detalhes = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
        //          ImageUrl = "https://dotnetmastery.com/bluevillaimages/villa4.jpg",
        //          Avaliar = 4,
        //          Ocupacao = 400,
        //          Sqft = 750,
        //          Cortesia = "",
        //          DataCriacao = DateTime.Now
        //      });
        //}    

        #endregion


    }
}
