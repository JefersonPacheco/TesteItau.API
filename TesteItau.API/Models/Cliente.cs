using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TesteItau.API.Models
{
    public class Cliente
    {
        [Key]
        public int idCliente { get; set; }
        public string nome { get; set; }
        public string sobrenome { get; set; }
        private string _cpf { get; set; }
        private string _cep { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string numLogradouro { get; set; }
        public List<Telefone> telefone { get; set; }

        public string cpf
        {
            get => _cpf;
            set
            {
                _cpf = limparString(value);
            }
        }

        public string cep
        {
            get => _cep;
            set
            {
                _cep = limparString(value);
            }
        }

        private string limparString(string value)
        {
            return value.Trim()
                        .Replace(" ", "")
                        .Replace("(", "")
                        .Replace(")", "")
                        .Replace("-", "")
                        .Replace(".", "")
                        .Replace(",", "")
                        .Replace(";", "");
        }
    }
}
