using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteItau.API.Context;
using TesteItau.API.Models;

namespace TesteItau.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClienteController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Cliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> Getclientes()
        {
            try
            {
                return await _context.cliente.Include(x => x.telefone).ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro inesperado ao buscar clientes: " + ex.Message });
            }
        }

        // GET: api/Cliente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            Cliente cliente = new Cliente();

            try 
            {
                cliente = await _context.cliente.Include(x => x.telefone).Where(x => x.idCliente == id).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return StatusCode(500, new { mensagem = "Erro inesperado ao buscar cliente" });
            }

            if (cliente == null)
            {
                return NotFound(new { mensagem = "Cliente não encontrado" });
            }

            return cliente;
        }

        // PUT: api/Cliente/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            var validacaoCliente = ValidarCliente(cliente);
            if (!validacaoCliente.Equals("Ok"))
            {
                return BadRequest(new { mensagem = validacaoCliente });
            }

            if (id != cliente.idCliente)
            {
                return BadRequest(new { mensagem = "Requesição inválida, ids divergentes" });
            }

            _context.Entry(cliente).State = EntityState.Modified;

            foreach(var item in cliente.telefone)
            {
                if(item.IdTelefone == 0)
                    _context.telefone.Add(item);
                else
                    _context.Entry(item).State = EntityState.Modified;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (!ClienteExists(id))
                {
                    return NotFound(new { mensagem = "Cliente não encontrado" });
                }
                else
                {
                    return StatusCode(500, new { mensagem = "Erro inesperado ao salvar a alteração " + ex.Message });
                }
            }

            return NoContent();
        }

        // POST: api/Cliente
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            var validacaoCliente = ValidarCliente(cliente);
            if (!validacaoCliente.Equals("Ok"))
            {
                return BadRequest(new { mensagem = validacaoCliente });
            }

            try
            {
                _context.cliente.Add(cliente);
                foreach(var item in cliente.telefone)
                {
                    _context.telefone.Add(item);
                }
                
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro inesperado ao salvar cliente: " + ex.Message });
            }

            return CreatedAtAction("GetCliente", new { id = cliente.idCliente }, cliente);
        }

        [HttpPost]
        [Route("pesquisar")]
        public async Task<IEnumerable<Cliente>> PesquisarCliente(Cliente cliente)
        {
            if (cliente == null)
            {
                return (IEnumerable<Cliente>)BadRequest(new { mensagem = "Favor preencher a busca" });
            }

            try
            {
                return await _context.cliente
                                     .Include(x => x.telefone)
                                     .Where(x => (string.IsNullOrEmpty(cliente.nome) || x.nome.Contains(cliente.nome)) &&
                                                 (string.IsNullOrEmpty(cliente.sobrenome) || x.sobrenome.Contains(cliente.sobrenome)) &&
                                                 (string.IsNullOrEmpty(cliente.cpf) || x.cpf == cliente.cpf))
                                     .ToListAsync();
            }
            catch (Exception)
            {
                return (IEnumerable<Cliente>)StatusCode(500, new { mensagem = "Erro inesperado ao buscar clientes" });
            }
        }

        // DELETE: api/Cliente/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cliente>> DeleteCliente(int id)
        {
            Cliente cliente = new Cliente();

            try
            {
                cliente = await _context.cliente.FindAsync(id);
            }
            catch (Exception)
            {
                return StatusCode(500, new { mensagem = "Erro inesperado ao buscar cliente" });
            }

            if (cliente == null)
                return NotFound(new { mensagem = "Cliente não encontrado" });

            try
            {
                _context.cliente.Remove(cliente);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return StatusCode(500, new { mensagem = "Erro inesperado ao excluir cliente" });
            }

            return cliente;
        }

        private bool ClienteExists(int id)
        {
            return _context.cliente.Any(e => e.idCliente == id);
        }

        private string ValidarCliente(Cliente cliente)
        {
            if (cliente == null)
            {
                return "Requesição inválida, o cliente não pode ser nulo";
            }
            else if(cliente.nome.Equals(""))
            {
                return "Requesição inválida, favor preencher o nome do cliente";
            }
            else if (cliente.sobrenome.Equals(""))
            {
                return "Requesição inválida, favor preencher o sobrenome do cliente";
            }
            else if (cliente.cpf.Equals(""))
            {
                return "Requesição inválida, favor preencher o CPF do cliente";
            }
            else if (cliente.cep.Equals(""))
            {
                return "Requesição inválida, favor preencher o CEP do cliente";
            }

            return "Ok";
        }
    }
}
