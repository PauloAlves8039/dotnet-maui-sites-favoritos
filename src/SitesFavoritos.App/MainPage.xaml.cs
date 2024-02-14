using SitesFavoritos.App.Models;
using SQLite;

namespace SitesFavoritos.App
{
    public partial class MainPage : ContentPage
    {
        private string _dbPath;
        SQLiteConnection _connection;
        Site site = new Site();
        
        public MainPage()
        {
            InitializeComponent();
        }

        private void CriarBancoDeDadosBtn_Clicked(object sender, EventArgs e)
        {
            _dbPath = Path.Combine(FileSystem.AppDataDirectory, "sites.db3");
            _connection = new SQLiteConnection(_dbPath);
            _connection.CreateTable<Site>();
            OperacoesVsl.IsVisible = true;
        }

        private void InserirBtn_Clicked(object sender, EventArgs e)
        {
            site.Endereco = ValorEnt.Text;
            _connection.Insert(site);
            LimparCampos();
            ListarSites();

        }

        private void AlterarBtn_Clicked(object sender, EventArgs e)
        {
            site.Id = Convert.ToInt32(IdEnt.Text);
            site.Endereco = ValorEnt.Text;
            _connection.Update(site);
            LimparCampos();
            ListarSites();

        }

        private void ExcluirBtn_Clicked(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(IdEnt.Text);
            _connection.Delete<Site>(id);
            LimparCampos();
            ListarSites();
        }

        private void LocalizarBtn_Clicked(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(IdEnt.Text);
            var sites = from s in _connection.Table<Site>()
                        where s.Id == id
                        select s;

            Site site = sites.First();
            IdEnt.Text = site.Id.ToString();
            ValorEnt.Text = site.Endereco;
        }

        private void ListarBtn_Clicked(object sender, EventArgs e)
        {
            LimparCampos();
            ListarSites();
        }

        private async void OnWebsiteLabelTapped(object sender, EventArgs e) 
        {
            var label = sender as Label;

            if (label != null && label.BindingContext is Site item) 
            {
                var websiteUrl = $"https://{item.Endereco.ToString()}";
                await Launcher.OpenAsync(new Uri(websiteUrl));
            }
        }

        private void TapGestureAlterarSite(object sender, TappedEventArgs e) 
        {
            var label = sender as Label;

            if (label != null && label.BindingContext is Site item) 
            {
                IdEnt.Text = item.Id.ToString();
                ValorEnt.Text = item.Endereco;
            } 
        }

        private void ListarSites() 
        {
            List<Site> listaDeSites = _connection.Table<Site>().ToList();
            ListaCv.ItemsSource = listaDeSites;
        }

        private void LimparCampos() 
        {
            IdEnt.Text = "";
            ValorEnt.Text = "";
        }
    }

}
