using Geradados.DataAccess.DB.Dtos;
using Geradados.DataAccess.Repository;
using GeraDados.DataModel.models;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace GeraDados.App;

public partial class frmUpload : Form
{
    Repository repository = new Repository();
    private IList<TipoContato> listaTipoContatos = new List<TipoContato>();

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
                foreach (PessoaJson pessoaJson in pessoas)
                {
                    if (repository.Pessoa.ObtemPessoaPorCPF(pessoaJson.CPF) != null)
                        continue;

                    string[] valoresContatos = { pessoaJson.Email, pessoaJson.Telefone_fixo, pessoaJson.Celular };
                    Pessoa pessoa = new Pessoa(pessoaJson.Nome, pessoaJson.CPF, pessoaJson.RG, pessoaJson.Sexo, Convert.ToDateTime(pessoaJson.Data_nasc));
                    List<Contato> contatos = Contato.Contatos(pessoa, listaTipoContatos, valoresContatos);
                    Endereco endereco = new Endereco(pessoa, pessoaJson.CEP, pessoaJson.Endereco, pessoaJson.Numero, pessoaJson.Bairro, pessoaJson.Cidade, pessoaJson.Estado);
                    CarteiraConfigurada carteiraConfigurada = CarteiraConfigurada.NovaCarteiraConfiguracao(
                        ObtemListaDeAtivosPorTipoDeAtivo((int)eTipoDeAtivo.Acao),
                        ObtemListaDeAtivosPorTipoDeAtivo((int)eTipoDeAtivo.FundoImobiliario));

                    repository.Pessoa.Salvar(pessoa);
                    foreach (Contato contato in contatos)
                        repository.Contato.Salvar(contato);
                    repository.Endereco.Salvar(endereco);
                    ValidaSeCampoCotaEstaZerado(carteiraConfigurada.Acoes, pessoa, carteiraConfigurada.ValorPorAcoes);
                    ValidaSeCampoCotaEstaZerado(carteiraConfigurada.Fiis, pessoa, carteiraConfigurada.ValorPorFiis);
                    repository.SaveChanges();
                }

            MessageBox.Show("Cadastro realizado com sucesso!");
            txtArquivo.Text = string.Empty;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
    #endregion

    #region Métodos void
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
        listaTipoContatos = repository.TipoContato.ObterTodos();
    }
    private void SalvarAtivosNoBancoDeDados(TipoDeAtivo? tipoDeAtivo)
    {
        if (tipoDeAtivo != null)
            if (tipoDeAtivo.ID.Equals((int)eTipoDeAtivo.Acao))
            {
                IList<AtivoJson>? acoes = LerArquivoJson<AtivoJson>(@"..\..\..\CargaDeAtivos\acoes.json");
                if (acoes != null)
                    SalvaListaDeAtivos(acoes.ToList(), tipoDeAtivo);
            }
            else if (tipoDeAtivo.ID.Equals((int)eTipoDeAtivo.FundoImobiliario))
            {
                IList<AtivoJson>? fii = LerArquivoJson<AtivoJson>(@"..\..\..\CargaDeAtivos\fiis.json");
                if (fii != null)
                    SalvaListaDeAtivos(fii.Where(fii => fii.Ultimo != "0").ToList(), tipoDeAtivo);
            }
    }
    private void SalvaListaDeAtivos(List<AtivoJson> ativos, TipoDeAtivo tipoDeAtivo)
    {
        foreach (AtivoJson item in ativos)
        {
            Ativo ativo = new Ativo(tipoDeAtivo, item.Ticker, item.Nome, ultimaNegociacao: $"{item.Ultimo},{item.Decimal}");
            repository.Ativo.Salvar(ativo);
            repository.SaveChanges();
        }
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
    private void ValidaSeCampoCotaEstaZerado(List<Ativo> Ativos, Pessoa pessoa, double valorPorAtivo)
    {
        foreach (var ativo in Ativos)
        {
            Carteira carteira = NovoAtivoParaCarteira(pessoa, ativo, valorPorAtivo);
            if (carteira.Cota > 0)
                repository.Carteira.Salvar(carteira);
        }
    }
    #endregion

    #region Métodos com return
    private IList<T>? LerArquivoJson<T>(string caminho)
    {
        return JsonConvert.DeserializeObject<IList<T>>(LeitorDeArquivo(caminho));
    }
    private string LeitorDeArquivo(string caminho)
    {
        StreamReader reader = new StreamReader(caminho);
        return reader.ReadToEnd();
    }
    private List<Ativo> ObtemListaDeAtivosPorTipoDeAtivo(int idTipoAtivo)
    {
        return repository.Ativo.ObtemAtivosPorTipoDeAtivo(repository.TipoDeAtivo.ObterPorId(idTipoAtivo));
    }
    private Carteira NovoAtivoParaCarteira(Pessoa pessoa, Ativo ativo, double valorPorAtivo)
    {
        return new Carteira()
        {
            Pessoa = pessoa,
            Ativo = ativo,
            Cota = Carteira.QuantidadeDeUmAtivo(valorPorAtivo, (double)ativo.UltimaNegociacao),
            DataCadastro = DateTime.Now,
            DataAtualizacao = DateTime.Now,
        };
    }
    #endregion
}