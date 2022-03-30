using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteItau.API.Context;
using TesteItau.API.Models;

namespace TesteItau.API.Repository
{
    public class ClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> getAllClientes()
        {
            return await _context.cliente.Include(x => x.telefone).ToListAsync();
        }
    }
}
