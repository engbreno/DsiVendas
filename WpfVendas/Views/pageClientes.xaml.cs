using DsiVendas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfVendas.ViewModels;
using WpfVendas.Views;

namespace WpfVendas.Pages
{
    /// <summary>
    /// Interação lógica para pageClientes.xam
    /// </summary>
    public partial class pageClientes : Page
    {
        private ClienteViewModel _viewModel;

        public pageClientes()
        {
            InitializeComponent();
            _viewModel = new ClienteViewModel();
            DataContext = _viewModel;
        }

        private async void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Clientes.Clear();
            await _viewModel.CarregarClientesDaAPI();
        }

        private void btnAddCliente_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void ClientesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Verifica se algum cliente está selecionado
            if (ClientesDataGrid.SelectedItem is Cliente clienteSelecionado)
            {
                // Cria a janela de edição
                var janelaCadastro = new cadCliente
                {
                    Owner = Window.GetWindow(this)  // Define o dono como a janela principal (MainWindow)
                };

                // Cria o ViewModel para a janela de edição, passando o cliente selecionado e a ação de fechar a janela
                var viewModel = new ClienteCadastroViewModel(janelaCadastro.Close, clienteSelecionado);
                // Define o DataContext da janela
                janelaCadastro.DataContext = viewModel;
                // Mostra a janela de edição modal (abre por cima da MainWindow)
                janelaCadastro.ShowDialog();
            }
        }
    }
}
