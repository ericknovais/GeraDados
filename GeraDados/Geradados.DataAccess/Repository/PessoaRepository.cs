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
    public class PessoaRepository : RepositoryBase<Pessoa>, IPessoaRepository
    {
        ContextoDataBase ctx = new ContextoDataBase();
        public PessoaRepository(ContextoDataBase contexto) : base(contexto)
        {
            ctx = contexto;
        }

        public Pessoa ObtemPessoaPorCPF(string cpf)
        {
            return ctx.Pessoas.FirstOrDefault(pessoa => pessoa.CPF.Equals(cpf));
        }
    }
}
