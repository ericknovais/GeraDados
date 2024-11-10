using Geradados.DataAccess.DB;
using GeraDados.DataModel.Models;
using GeraDados.DataModel.Repositories;

namespace Geradados.DataAccess.Repositories
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