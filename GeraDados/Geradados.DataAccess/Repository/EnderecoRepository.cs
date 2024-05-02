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
    public class EnderecoRepository : RepositoryBase<Endereco>, IEnderecoRepository
    {
        ContextoDataBase ctx = new ContextoDataBase();
        public EnderecoRepository(ContextoDataBase contexto) : base(contexto)
        {
            ctx = contexto;
        }

        public Endereco ObtemEnderecoPorIdPessoa(int idPessoa)
        {
            return ctx.Enderecos.FirstOrDefault(endereco => endereco.Pessoa.ID.Equals(idPessoa));
        }
    }
}
