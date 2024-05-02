using Geradados.DataAccess.DB;
using GeraDados.DataModel.models;
using GeraDados.DataModel.repository;

namespace Geradados.DataAccess.Repository;

public class TipoContatoRepository : RepositoryBase<TipoContato>, ITipoContatoRepository
{
    ContextoDataBase ctx = new ContextoDataBase();
    public TipoContatoRepository(ContextoDataBase contexto) : base(contexto)
    {
        ctx = contexto;
    }
}
