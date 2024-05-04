using System.ComponentModel.DataAnnotations.Schema;

namespace GeraDados.DataModel.models
{
    [Table("Carteiras")]
    public class Carteira : EntityBase
    {
        public Carteira()
        {
            Pessoa = new Pessoa();
            Ativo = new Ativo();
        }
        public Pessoa Pessoa { get; set; }
        public Ativo Ativo { get; set; }
        public int Cota { get; set; }

        public override void Valida()
        {
            ValidaCampoNumerico(Cota, "Cota");
            base.Valida();
        }
    }
}