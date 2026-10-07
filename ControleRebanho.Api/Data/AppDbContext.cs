using ControleRebanho.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleRebanho.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Produtor> Produtores { get; set; }
    }
}