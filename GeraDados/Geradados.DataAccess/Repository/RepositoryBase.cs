using Geradados.DataAccess.DB;
using GeraDados.DataModel.models;
using GeraDados.DataModel.repository;

namespace Geradados.DataAccess.Repository;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
{
    ContextoDataBase ctx;
    public RepositoryBase(ContextoDataBase contexto)
    {
        ctx = contexto;
    }

    public void Excluir(T entity)
    {
       ctx.Set<T>().Remove(entity);
    }

    public T? ObterPorId(int id)
    {
        return ctx.Set<T>().FirstOrDefault(entity => entity.ID.Equals(id));
    }

    public IList<T> ObterTodos()
    {
        return ctx.Set<T>().ToList();
    }

    public void Salvar(T entity)
    {
        if(entity.ID.Equals(0))
            ctx.Set<T>().Add(entity);
    }
}