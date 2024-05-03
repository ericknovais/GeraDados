using Geradados.DataAccess.DB.Dtos;
using Geradados.DataAccess.Repository;
using GeraDados.DataModel.models;
using Newtonsoft.Json;

namespace GeraDados.App;

public partial class frmUpload : Form
{
    Repository repository = new Repository();

    #region Métodos Do Forms
    public frmUpload()
    {
        InitializeComponent();
        InicializaDadosNoBanco();
    }
    private void btnUpload_Click(object sender, EventArgs e)
    {
        OpenFileDialog ofd = new OpenFileDialog();
        ofd.CheckFileExists = true;
        ofd.Multiselect = false;
        if (ofd.ShowDialog() == DialogResult.OK)
            txtArquivo.Text = ofd.FileName;
    }
    private void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            IList<PessoaJson>? pessoas = LerArquivoJson<PessoaJson>(txtArquivo.Text);

            if (pessoas != null)
                foreach (var item in pessoas)
                {
                    if (repository.Pessoa.ObtemPessoaPorCPF(item.CPF) != null)
                        continue;

                    Pessoa pessoa = NovaPessoa(item);
                    List<Contato> contatos = ContatosPessoa(item, pessoa);
                    Endereco endereco = EnderecoPessoa(item, pessoa);

                    repository.Pessoa.Salvar(pessoa);
                    foreach (Contato contato in contatos)
                        repository.Contato.Salvar(contato);
                    repository.Endereco.Salvar(endereco);

                    repository.SaveChanges();
                }

            MessageBox.Show("Salvo");
            txtArquivo.Text = string.Empty;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
    #endregion

    #region Métodos privados
    private void InicializaDadosNoBanco()
    {
        if (repository.TipoContato.ObterTodos().Count.Equals(0))
            SalvaTipoDeContatosNoBanco();
        if (repository.TipoDeAtivo.ObterTodos().Count.Equals(0))
            SalvaTipoDeAtivosNoBanco();
        TipoDeAtivo? acao = repository.TipoDeAtivo.ObterPorId((int)eTipoDeAtivo.Acao);
        if (repository.Ativo.ObtemAtivosPorTipoDeAtivo(acao).Count.Equals(0))
            SalvarAtivosNoBancoDeDados(acao);
        TipoDeAtivo? fii = repository.TipoDeAtivo.ObterPorId((int)eTipoDeAtivo.FundoImobiliario);
        if (repository.Ativo.ObtemAtivosPorTipoDeAtivo(fii).Count.Equals(0))
            SalvarAtivosNoBancoDeDados(fii);
    }
    private void SalvarAtivosNoBancoDeDados(TipoDeAtivo? tipoDeAtivo)
    {
        if (tipoDeAtivo != null)
            if (tipoDeAtivo.ID.Equals((int)eTipoDeAtivo.Acao))
            {
                IList<AtivoJson>? acoes = LerArquivoJson<AtivoJson>(@"C:\\Users\\erick\\Downloads\\csvAcoesjson.json");
                if (acoes != null)
                    foreach (AtivoJson item in acoes)
                    {
                        Ativo ativo = NovoAtivo(item, tipoDeAtivo);
                        repository.Ativo.Salvar(ativo);
                        repository.SaveChanges();
                    }
            }
            else if (tipoDeAtivo.ID.Equals((int)eTipoDeAtivo.FundoImobiliario))
            {
                IList<AtivoJson>? fii = LerArquivoJson<AtivoJson>(@"C:\\Users\\erick\\Downloads\\csvFiisjson.json");
                if (fii != null)
                    foreach (AtivoJson item in fii.Where(fii => fii.Ultimo != "0").ToList())
                    {
                        Ativo ativo = NovoAtivo(item, tipoDeAtivo);
                        repository.Ativo.Salvar(ativo);
                        repository.SaveChanges();
                    }
            }
    }
    private Ativo NovoAtivo(AtivoJson? item, TipoDeAtivo? tipoDeAtivo)
    {
        Ativo ativo = new Ativo()
        {
            TipoDeAtivo = tipoDeAtivo,
            Nome = item.Nome,
            Ticker = item.Ticker,
            UltimaNegociacao = Convert.ToDecimal($"{item.Ultimo},{item.Decimal}"),
            DataCadastro = DateTime.Now,
            DataAtualizacao = DateTime.Now,
        };

        ativo.Valida();
        return ativo;
    }
    private void SalvaTipoDeAtivosNoBanco()
    {
        List<TipoDeAtivo> listaTipoDeAtivos = new TipoDeAtivo().CarregaTipoDeAtivo();
        foreach (TipoDeAtivo tipoDeAtivo in listaTipoDeAtivos)
            repository.TipoDeAtivo.Salvar(tipoDeAtivo);
        repository.SaveChanges();
    }
    private void SalvaTipoDeContatosNoBanco()
    {
        List<TipoContato> listadeTipocontatos = new TipoContato().CarregaListaTipoContato();
        foreach (TipoContato tipoContato in listadeTipocontatos)
            repository.TipoContato.Salvar(tipoContato);
        repository.SaveChanges();
    }
    private IList<T>? LerArquivoJson<T>(string caminho)
    {
        return JsonConvert.DeserializeObject<IList<T>>(LeitorDeArquivo(caminho));
    }
    private string LeitorDeArquivo(string caminho)
    {
        StreamReader reader = new StreamReader(caminho);
        return reader.ReadToEnd();
    }
    private Endereco EnderecoPessoa(PessoaJson pessoaJson, Pessoa pessoa)
    {
        Endereco endereo = new Endereco()
        {
            Pessoa = pessoa,
            Bairro = pessoaJson.Bairro,
            CEP = pessoaJson.CEP,
            Cidade = pessoaJson.Cidade,
            Estado = pessoaJson.Estado,
            Logradouro = pessoaJson.Endereco,
            Numero = pessoaJson.Numero,
            DataCadastro = DateTime.Now,
            DataAtualizacao = DateTime.Now
        };

        endereo.Valida();
        return endereo;
    }
    private Pessoa NovaPessoa(PessoaJson pessoaJson)
    {
        var pessoa = new Pessoa()
        {
            Nome = pessoaJson.Nome,
            CPF = pessoaJson.CPF,
            RG = pessoaJson.RG,
            Sexo = pessoaJson.Sexo,
            DataNascimento = Convert.ToDateTime(pessoaJson.Data_nasc),
            DataCadastro = DateTime.Now,
            DataAtualizacao = DateTime.Now
        };
        pessoa.Valida();
        return pessoa;
    }
    private List<Contato> ContatosPessoa(PessoaJson pessoaJson, Pessoa pessoa)
    {
        List<Contato> contatos = new List<Contato>()
        {
            new Contato()
            {
                Pessoa = pessoa,
                TipoContato = repository.TipoContato.ObterPorId((int)eTipoContato.Email),
                Valor = pessoaJson.Email,
                DataCadastro = DateTime.Now,
                DataAtualizacao = DateTime.Now
            },
             new Contato()
            {
                Pessoa = pessoa,
                TipoContato = repository.TipoContato.ObterPorId((int)eTipoContato.Fixo),
                Valor = pessoaJson.Telefone_fixo,
                DataCadastro = DateTime.Now,
                DataAtualizacao = DateTime.Now
            },
            new Contato()
            {
                Pessoa = pessoa,
                TipoContato = repository.TipoContato.ObterPorId((int)eTipoContato.Celular),
                Valor = pessoaJson.Celular,
                DataCadastro = DateTime.Now,
                DataAtualizacao = DateTime.Now
            }
        };
        foreach (var item in contatos)
            item.Valida();
        return contatos;
    }
    #endregion
}
