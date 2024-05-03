using System.ComponentModel.DataAnnotations.Schema;

namespace GeraDados.DataModel.models;

[Table("TipoDeAtivos")]
public class TipoDeAtivo : EntityBase
{
    public TipoDeAtivo()
    {
        Descricao = string.Empty;
    }

    public string Descricao { get; set; }

    public override void Valida()
    {
        ValidaCampoTexto(Descricao, "Descrição");
        base.Valida();
    }

    public List<TipoDeAtivo> CarregaTipoDeAtivo()
    {
        return new List<TipoDeAtivo>()
        {
            new TipoDeAtivo()
            {
                Descricao = "Ação",
                DataCadastro = DateTime.Now,
                DataAtualizacao = DateTime.Now,
            },
            new TipoDeAtivo()
            {
                Descricao = "Fundo Imobiliario",
                DataCadastro = DateTime.Now,
                DataAtualizacao =  DateTime.Now
            }
        };
    }
}
