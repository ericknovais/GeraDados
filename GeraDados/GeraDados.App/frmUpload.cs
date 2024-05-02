using Geradados.DataAccess.DB.Dtos;
using Geradados.DataAccess.Repository;
using GeraDados.DataModel.models;
using Newtonsoft.Json;
using System.Runtime.InteropServices;


namespace GeraDados.App;

public partial class frmUpload : Form
{
    Repository repository = new Repository();

    public frmUpload()
    {
        InitializeComponent();
        if (repository.TipoContato.ObterTodos().Count.Equals(0))
            CarregaTipoDeContatos();
    }

    private void CarregaTipoDeContatos()
    {
        var listadeTipocontatos = new TipoContato().CarregaListaTipoContato();
        foreach (var tipoContato in listadeTipocontatos)
            repository.TipoContato.Salvar(tipoContato);

        repository.SaveChanges();
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
            StreamReader reader = new StreamReader(txtArquivo.Text);
            string json = reader.ReadToEnd();
            IList<PessoaJson> pessoas = JsonConvert.DeserializeObject<IList<PessoaJson>>(json);

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
            MessageBox.Show(ex.Message);
        }
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
                TipoContato = repository.TipoContato.ObterPorId((int)TipoContatos.Email),
                Valor = pessoaJson.Email,
                DataCadastro = DateTime.Now,
                DataAtualizacao = DateTime.Now
            },
             new Contato()
            {
                Pessoa = pessoa,
                TipoContato = repository.TipoContato.ObterPorId((int)TipoContatos.Fixo),
                Valor = pessoaJson.Telefone_fixo,
                DataCadastro = DateTime.Now,
                DataAtualizacao = DateTime.Now
            },
            new Contato()
            {
                Pessoa = pessoa,
                TipoContato = repository.TipoContato.ObterPorId((int)TipoContatos.Celular),
                Valor = pessoaJson.Celular,
                DataCadastro = DateTime.Now,
                DataAtualizacao = DateTime.Now
            }
        };
        foreach (var item in contatos)
            item.Valida();
        return contatos;
    }
}
