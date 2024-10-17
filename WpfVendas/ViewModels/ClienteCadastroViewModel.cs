using DsiVendas.Models;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace WpfVendas.ViewModels
{
    internal class ClienteCadastroViewModel : INotifyPropertyChanged
    {

        private readonly HttpClient _httpClient;
        private Cliente _cliente;
        public Cliente Cliente
        {
            get => _cliente;
            set
            {
                _cliente = value;
                OnPropertyChanged();
            }
        }

        public ICommand SalvarCommand { get; }
        public ICommand CancelarCommand { get; }

        private readonly Action _fecharAction;

        public ClienteCadastroViewModel(Action fecharAction, Cliente cliente = null)
        {
            _fecharAction = fecharAction;
            Cliente = cliente ?? new Cliente(); // Se o cliente for null, criamos um novo.
            SalvarCommand = new RelayCommand(SalvarCliente);
            CancelarCommand = new RelayCommand(Cancelar);
        }

        private void SalvarCliente(object obj)
        {
            if (Cliente != null)
            {
                if (Cliente.Id == 0)
                {
                    CriarCliente(Cliente);
                }
                else
                {
                    AtualizarClienteAsync(Cliente);
                }

                //_fecharAction();
            }
        }

        private void CriarCliente(Cliente cliente)
        {
            // Lógica para criação do cliente (incluir cliente na base de dados, etc.)
        }

        private async Task AtualizarClienteAsync(Cliente cliente)
        {
            try
            {
                var apiUrl = $"http://localhost:3000/Api/UpdateCliente/{cliente.Id}";
                var response = await _httpClient.PutAsJsonAsync(apiUrl, cliente);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Cliente {cliente.Nome} atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                         }
                else
                {
                    MessageBox.Show($"Erro ao salvar o cliente: {response.StatusCode}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                // Captura exceções e mostra a mensagem de erro
                Console.WriteLine($"Erro ao atualizar o cliente: {ex.Message}");
            }
        }

        private void Cancelar(object obj)
        {
            _fecharAction(); // Fecha a janela sem salvar
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
