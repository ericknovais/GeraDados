using GeraDados.DataModel.models;

namespace GeraDados.DataModel.repository;

public interface IRepositoryBase<T> where T : EntityBase
{
    void Salvar(T entity);
    void Excluir(T entity);
    T? ObterPorId(int id);
    IList<T> ObterTodos();
}
