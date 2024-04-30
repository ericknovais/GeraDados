using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace GeraDados.DataModel.models
{
    [Table("Contatos")]
    public class Contato : EntityBase
    {
        public Pessoa Pessoa { get; set; }
        public int IdPessoa { get; set; }
        public TipoContato TipoContato { get; set; }
        public int IdTipoContato { get; set; }
        public string Valor { get; set; }

        public override void Valida()
        {
            var descricaoCampo = DescricaoCampo(IdTipoContato);
            ValidaCampoTexto(Valor, descricaoCampo);
            if (IdTipoContato.Equals((int)TipoContatos.Email))
                ValidaEmail();
            base.Valida();
        }

        private string DescricaoCampo(int idTipoContato)
        {
            var descricaoCampo = string.Empty;
            switch (idTipoContato) 
            {
                case (int)TipoContatos.Email:
                    descricaoCampo = "Email";
                    break;
                case (int)TipoContatos.Fixo:
                    descricaoCampo = "Telefone Fixo";
                    break;
                case (int)TipoContatos.Celular:
                    descricaoCampo = "Celular";
                    break;
            }
            return descricaoCampo;
        }

        private void ValidaEmail() 
        {
            if (!Valor.Contains("@") || !Valor.Contains(".com"))
                _msgErro.Append($"E-mail com formato invalido! {Environment.NewLine}");
        }
    }
}
