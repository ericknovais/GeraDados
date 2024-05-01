using Geradados.DataAccess.DB;
using GeraDados.DataModel.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geradados.DataAccess.Repository
{
    public class Repository : IRepository
    {
        ContextoDataBase ctx;
        public Repository()
        {
            ctx = new ContextoDataBase();
        }
        public IPessoaRepository Pessoa => throw new NotImplementedException();

        public IEnderecoRepository Endereco => throw new NotImplementedException();

        public IContatoRepository Contato => throw new NotImplementedException();

        public ITipoContato TipoContato => throw new NotImplementedException();

        public void SaveChanges()
        {
            ctx.SaveChanges();
        }
    }
}
