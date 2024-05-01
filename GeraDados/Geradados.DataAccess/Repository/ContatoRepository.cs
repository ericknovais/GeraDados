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
    public class ContatoRepository : RepositoryBase<Contato>, IContatoRepository
    {
        ContextoDataBase ctx = new ContextoDataBase();
        public ContatoRepository(ContextoDataBase contexto) : base(contexto)
        {
            ctx = contexto;
        }

        IList<Contato> IContatoRepository.ObtemContatosPorIdPessoa(int idPessoa)
        {
            return ctx.Contatos.Where(contato => contato.IdPessoa.Equals(idPessoa)).ToList();
        }
    }
}
