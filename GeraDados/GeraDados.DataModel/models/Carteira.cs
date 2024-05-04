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

        public int InicializaValorInicialDaPessoa()
        {
            Random random = new Random();
            return random.Next(200, 1000000);
        }

        public Double PorcentagelDoValorParaUmTipoDeAtivo(int valorInicial)
        {
            Random random = new Random();
            double porcentagem = Convert.ToDouble(random.Next(1, 100) / 100.00);
            return ((Double)valorInicial * (porcentagem));
        }
    }
}