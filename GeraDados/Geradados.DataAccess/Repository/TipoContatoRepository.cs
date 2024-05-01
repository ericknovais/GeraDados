using Geradados.DataAccess.DB;
using GeraDados.DataModel.models;
using GeraDados.DataModel.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geradados.DataAccess.Repository
{
    public class TipoContatoRepository : RepositoryBase<TipoContato>, ITipoContatoRepository
    {
        ContextoDataBase ctx = new ContextoDataBase();
        public TipoContatoRepository(ContextoDataBase contexto) : base(contexto)
        {
            ctx = contexto;
        }
    }
}
