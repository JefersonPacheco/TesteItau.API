using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TesteItau.API.Models
{
    public class Telefone
    {
        [Key]
        public int IdTelefone { get; set; }
        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }
        public string TipoTelefone { get; set; }
        private string _DDD { get; set; }
        private string _Numero { get; set; }
        [JsonIgnore]
        public Cliente Cliente { get; set; }

        public string DDD
        {
            get => _DDD;
            set
            {
                _DDD = limparString(value);
            }
        }

        public string Numero
        {
            get => _Numero;
            set
            {
                _Numero = limparString(value);
            }
        }

        private string limparString(string value)
        {
            return value.Trim()
                        .Replace("_", "")
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
