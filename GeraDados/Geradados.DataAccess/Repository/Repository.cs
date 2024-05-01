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

        IPessoaRepository pessoa;
        public IPessoaRepository Pessoa { get { return pessoa ?? (pessoa = new PessoaRepository(ctx)); } }

        IEnderecoRepository endereco;
        public IEnderecoRepository Endereco { get { return endereco ?? (endereco = new EnderecoRepository(ctx)); } }

        IContatoRepository contato;
        public IContatoRepository Contato { get { return contato ?? (contato = new ContatoRepository(ctx)); } }

        ITipoContatoRepository tipoContato;
        public ITipoContatoRepository TipoContato { get { return tipoContato ?? (tipoContato = new TipoContatoRepository(ctx)); } }

        public void SaveChanges()
        {
            ctx.SaveChanges();
        }
    }
}
