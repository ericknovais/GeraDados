using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeraDados.DataModel.models
{
    public abstract class EntityBase
    {
        public int ID { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }

        protected StringBuilder _msgErro = new StringBuilder();

        public virtual void Valida()
        {
            if(_msgErro.Length > 0) 
                throw new Exception(_msgErro.ToString());
        }

        public void ValidaCampoTexto(string valorCampo, string nomeCampo) 
        {
            if (string.IsNullOrEmpty(valorCampo))
                _msgErro.Append($"O campo {nomeCampo} é obrigatório! {Environment.NewLine}");
        }
    }
}
