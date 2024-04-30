using GeraDados.DataModel.models;

namespace GeraDados.DataModel.repository;

public interface IPessoaRepository: IRepositoryBase<Pessoa>
{
    Pessoa ObtemPessoaPorCPF(string cpf);
}
