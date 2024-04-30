using System.ComponentModel.DataAnnotations.Schema;

namespace GeraDados.DataModel.models;

[Table("TipoContatos")]
public class TipoContato : EntityBase
{
    public string Descricao { get; set; }

    public List<TipoContato> CarregaListaTipoContato() 
    {
        List<TipoContato> tipoContatos = new List<TipoContato>()
        {
            new TipoContato() { 
                Descricao = "Email",
                DataCadastro = DateTime.Now,
                DataAtualizacao = DateTime.Now,
            },
            new TipoContato() {
                Descricao = "Telefone Fixo",
                DataCadastro = DateTime.Now,
                DataAtualizacao = DateTime.Now,
            },
            new TipoContato() {
                Descricao = "Celular",
                DataCadastro = DateTime.Now,
                DataAtualizacao = DateTime.Now,
            }
        };
        return tipoContatos;
    }
}