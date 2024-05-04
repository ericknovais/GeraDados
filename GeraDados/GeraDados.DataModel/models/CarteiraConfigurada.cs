using System.ComponentModel.DataAnnotations.Schema;

namespace GeraDados.DataModel.models;

[NotMapped]
public class CarteiraConfigurada
{
    public CarteiraConfigurada()
    {
        Acoes = new List<Ativo>();
        Fiis = new List<Ativo>();

    }
    public int ValorInicial { get; set; }
    public double ValorParaAcoes { get; set; }
    public int QuantidadeDeAcoes { get; set; }
    public double ValorPorAcoes { get; set; }
    public double ValorParaFiis { get; set; }
    public int QuantidaDeFiis { get; set; }
    public double ValorPorFiis { get; set; }
    public int ValorTotal { get; set; }
    public List<Ativo> Acoes { get; set; }
    public List<Ativo> Fiis { get; set; }
}
