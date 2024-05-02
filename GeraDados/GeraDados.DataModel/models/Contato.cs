using System.ComponentModel.DataAnnotations.Schema;

namespace GeraDados.DataModel.models;

[Table("Contatos")]
public class Contato : EntityBase
{
    public Pessoa Pessoa { get; set; }
    public TipoContato TipoContato { get; set; }
    public string Valor { get; set; }

    public override void Valida()
    {
        var descricaoCampo = DescricaoCampo(TipoContato.ID);
        ValidaCampoTexto(Valor, descricaoCampo);
        if (TipoContato.ID.Equals((int)TipoContatos.Email))
            ValidaEmail();
        base.Valida();
    }

    private string DescricaoCampo(int idTipoContato)
    {
        var descricaoCampo = string.Empty;
        switch (idTipoContato) 
        {
            case (int)TipoContatos.Email:
                descricaoCampo = "E-mail";
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