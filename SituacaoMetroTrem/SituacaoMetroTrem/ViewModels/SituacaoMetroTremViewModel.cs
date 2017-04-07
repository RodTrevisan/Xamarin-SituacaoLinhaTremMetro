using System.ComponentModel;
using System.Collections.ObjectModel;
using SituacaoMetroTrem.Models;
using System.Threading.Tasks;
using System;
using SituacaoMetroTrem.Interfaces;
using SituacaoMetroTrem.Services;
using Xamarin.Forms;

namespace SituacaoMetroTrem.ViewModels
{
    public class SituacaoMetroTremViewModel : INotifyPropertyChanged
    {
        private bool busy;
        private IRestClient<SituacaoMetroTremModel> _restClient;

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<SituacaoMetroTremModel> SituacoesMetroTrem { get; set; }
        public Command GetCatsCommand { get; set; }
        public bool IsBusy {
            get { return busy; }
            set
            {
                busy = value;
                OnPropertyChanged();
                GetCatsCommand.ChangeCanExecute();
            }
        }

        public SituacaoMetroTremViewModel()
        {
            SituacoesMetroTrem = new ObservableCollection<SituacaoMetroTremModel>();
            _restClient = new RestClient<SituacaoMetroTremModel>();

            GetCatsCommand = new Command(async () => await ObterSituacaoMetroTrem(), () => !IsBusy);
        }

        private void OnPropertyChanged(
            [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null) => 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        async Task ObterSituacaoMetroTrem()
        {
            if (!IsBusy)
            {
                Exception Error = null;
                try
                {
                    IsBusy = true;

                    var result = await _restClient.HttpGetMethod("http://situacaometroviaria.azurewebsites.net/situacaoatual");
                    SituacoesMetroTrem.Clear();

                    foreach (var item in result)
                    {
                        SituacoesMetroTrem.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    Error = ex;
                }
                finally
                {
                    if (Error != null)
                    {
                        await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(
                        "Error!", Error.Message, "OK");
                    }

                    IsBusy = false;
                }
            }
            return;
        }
    }
}
