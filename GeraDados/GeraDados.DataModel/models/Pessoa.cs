using System.ComponentModel.DataAnnotations.Schema;

namespace GeraDados.DataModel.models;

[Table("Pessoas")]
public class Pessoa : EntityBase
{
    public string Nome { get; set; }
    public string CPF { get; set; }
    public string RG { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Sexo { get; set; }

    public override void Valida()
    {
        ValidaCampoTexto(Nome, "Nome Completo");
        ValidaCampoTexto(CPF, "CPF");
        ValidaCampoTexto(RG, "RG");
        ValidaCampoTexto(Sexo, "Sexo");
        base.Valida();
    }
}