using GeraDados.DataModel.models;

namespace GeraDados.DataModel.repository;

public interface IEnderecoRepository: IRepositoryBase<Endereco>
{
    Endereco? ObtemEnderecoPorIdPessoa(int idPessoa);
}
