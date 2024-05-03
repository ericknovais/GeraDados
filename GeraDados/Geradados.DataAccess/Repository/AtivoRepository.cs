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
    public class AtivoRepository : RepositoryBase<Ativo>, IAtivoRepository
    {
        ContextoDataBase ctx = new ContextoDataBase();
        public AtivoRepository(ContextoDataBase contexto) : base(contexto)
        {
            ctx = contexto;
        }

        public List<Ativo> ObtemAtivosPorTipoDeAtivo(TipoDeAtivo? tipoDeAtivo)
        {
            if (tipoDeAtivo != null)
                return ctx.Ativos.Where(ativo => ativo.TipoDeAtivo.ID.Equals(tipoDeAtivo.ID)).ToList();
            return new List<Ativo>();
        }
    }
}
