using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class Filtragem : ContentPage
{
    ObservableCollection<Produto> produtosFiltrados = new ObservableCollection<Produto>();

    public Filtragem()
    {
        InitializeComponent();
        listaProdutos.ItemsSource = produtosFiltrados;
    }

    private async void Filtrar_Clicked(object sender, EventArgs e)
    {
        DateTime dataInicio = datePickerInicio.Date;
        DateTime dataFim = datePickerFim.Date.AddHours(23).AddMinutes(59).AddSeconds(59);

        try
        {
            List<Produto> resultado = await App.Db.ListarProdutosPorPeriodoAsync(dataInicio, dataFim);

            produtosFiltrados.Clear();
            resultado.ForEach(p => produtosFiltrados.Add(p));

            if (!resultado.Any())
            {
                await DisplayAlert("Busca Inconsistente", "Nenhum produto encontrado nesse período.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Ocorreu um erro: {ex.Message}", "OK");
        }
    }
}
