using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeraDados.DataModel.models
{
    [Table("Enderecos")]
    public class Endereco : EntityBase
    {
        public Pessoa Pessoa { get; set; }
        public int IdPessoa { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; } = 0;
        public string  Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public override void Valida()
        {
            ValidaCampoTexto(CEP, "CEP");
            ValidaCampoTexto(Logradouro, "Logradouro");
            ValidaCampoNumerico(Numero, "Número");
            ValidaCampoTexto(Bairro, "Bairro");
            ValidaCampoTexto(Cidade, "Cidade");
            ValidaCampoTexto(Estado, "Estado");
            base.Valida();
        }
    }
}
