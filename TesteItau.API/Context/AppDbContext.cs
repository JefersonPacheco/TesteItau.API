using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteItau.API.Models;

namespace TesteItau.API.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Cliente> cliente { get; set; }
        public DbSet<Telefone> telefone { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().HasIndex(x => x.idCliente);
            modelBuilder.Entity<Telefone>().HasIndex(x => x.IdTelefone);

  
        }
    }
}
