using GeraDados.DataModel.models;

namespace GeraDados.DataModel.repository;

public interface IContatoRepository:IRepositoryBase<Contato>
{
    IList<Contato> ObtemContatosPorIdPessoa(int IdPessoa);
}
