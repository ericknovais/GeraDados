using System.ComponentModel.DataAnnotations.Schema;

namespace GeraDados.DataModel.models;

[Table("Ativos")]
public class Ativo : EntityBase
{
    public Ativo()
    {
        TipoDeAtivo = new TipoDeAtivo();
        Ticker = string.Empty;
        Nome = string.Empty;
    }

    public TipoDeAtivo? TipoDeAtivo { get; set; }
    public string Ticker { get; set; }
    public string Nome { get; set; }
    public decimal UltimaNegociacao { get; set; }

    public override void Valida()
    {
        ValidaCampoTexto(Ticker, "Ticker");
        ValidaCampoTexto(Nome, "Nome");
        base.Valida();
    }
}