using DsiVendas.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfVendas.ViewModels
{
    public class ClienteViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient;

        public ObservableCollection<Cliente> Clientes { get; set; }

        public ClienteViewModel()
        {
            _httpClient = new HttpClient();
            Clientes = new ObservableCollection<Cliente>();
            //CarregarClientesDaAPI();
        }

        public async Task CarregarClientesDaAPI()
        {
            try
            {
                var apiUrl = "http://localhost:5299/Api/GetClientes";
                var clientesDaApi = await _httpClient.GetFromJsonAsync<Cliente[]>(apiUrl);

                if (clientesDaApi != null)
                {
                    foreach (var cliente in clientesDaApi)
                    {
                        Clientes.Add(cliente);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Erro ao buscar clientes: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
