using System.Collections.ObjectModel;
using Xamarin.Forms;
using SituacaoMetroTrem.Models;
using SituacaoMetroTrem.Services;
using SituacaoMetroTrem.Interfaces;

namespace SituacaoMetroTrem
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<SituacaoMetroTremModel> SituacoesMetroTremModel { get; set; }
        private IRestClient<SituacaoMetroTremModel> _restClient;

        public MainPage()
        {
            SituacoesMetroTremModel = new ObservableCollection<SituacaoMetroTremModel>();
            _restClient = new RestClient<SituacaoMetroTremModel>();

            InitializeComponent();
        }

        public async void Load()
        {

        }
    }
}
