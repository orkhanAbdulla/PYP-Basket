using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PYP_FtontToBack.Models;

namespace PYP_FtontToBack.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Product> Products {get;set; }
        public DbSet<ProductPhoto> ProductPhotos { get; set; }


    }
}
