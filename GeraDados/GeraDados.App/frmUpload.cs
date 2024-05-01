using Geradados.DataAccess.DB.Dtos;
using Geradados.DataAccess.Repository;
using GeraDados.DataModel.models;
using Newtonsoft.Json;


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
        StreamReader reader = new StreamReader(txtArquivo.Text);
        string json = reader.ReadToEnd();
        IList<PessoaJson> pessoas = JsonConvert.DeserializeObject<IList<PessoaJson>>(json);

        foreach (var item in pessoas)
        {
            Pessoa pessoa = new Pessoa()
            {
                CPF = item.CPF,
            };
        }
    }
}
