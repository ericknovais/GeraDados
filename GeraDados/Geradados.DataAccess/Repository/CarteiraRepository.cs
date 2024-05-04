using Geradados.DataAccess.DB;
using GeraDados.DataModel.models;
using GeraDados.DataModel.repository;

namespace Geradados.DataAccess.Repository
{
    public class CarteiraRepository : RepositoryBase<Carteira>, ICarteiraRepository
    {
        ContextoDataBase ctx = new ContextoDataBase();
        public CarteiraRepository(ContextoDataBase contexto) : base(contexto)
        {
            ctx = contexto;
        }
    }
}