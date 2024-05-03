using System.ComponentModel.DataAnnotations.Schema;

namespace GeraDados.DataModel.models;

[Table("Contatos")]
public class Contato : EntityBase
{
    public Contato()
    {
        Pessoa = new Pessoa();
        TipoContato = new TipoContato();
        Valor = string.Empty;
    }

    public Pessoa Pessoa { get; set; }
    public TipoContato? TipoContato { get; set; }
    public string Valor { get; set; }

    public override void Valida()
    {
        var descricaoCampo = DescricaoCampo(TipoContato);
        ValidaCampoTexto(Valor, descricaoCampo);
        if (TipoContato != null && TipoContato.ID.Equals((int)eTipoContato.Email))
            ValidaEmail();
        base.Valida();
    }

    private string DescricaoCampo(TipoContato? tipoContato)
    {
        var descricaoCampo = string.Empty;
        if (tipoContato != null)
            switch (tipoContato.ID)
            {
                case (int)eTipoContato.Email:
                    descricaoCampo = "E-mail";
                    break;
                case (int)eTipoContato.Fixo:
                    descricaoCampo = "Telefone Fixo";
                    break;
                case (int)eTipoContato.Celular:
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