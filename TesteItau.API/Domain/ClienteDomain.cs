using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteItau.API.Context;
using TesteItau.API.Models;
using TesteItau.API.Repository;

namespace TesteItau.API.Domain
{
    public class ClienteDomain
    {
        ClienteRepository _repository;

        public ClienteDomain(AppDbContext context)
        {
           _repository = new ClienteRepository(context);
        }

        public async Task<IEnumerable<Cliente>> getAllClientes()
        {
            return await _repository.getAllClientes();
        }
    }
}
