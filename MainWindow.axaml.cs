using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace katalog;

public partial class MainWindow : Window
{
    private readonly ListScreen ListView = new();
    private readonly DetailScreen DetailView = new();
    private readonly AddScreen AddView = new();

    public MainWindow()
    {
        InitializeComponent();
        ShowList();
    }

    private void OnListClick(object? sender, RoutedEventArgs e)
    {
        ShowList();
    }

    private void OnDetailClick(object? sender, RoutedEventArgs e)
    {
        ShowDetail();
    }

    private void OnAddClick(object? sender, RoutedEventArgs e)
    {
        ShowAdd();
    }

    private void OnCountClick(object? sender, RoutedEventArgs e)
    {
        CountText.Text = BuildCountText(ReadLineCount());
    }

    private int ReadLineCount()
    {
        var lines = File.ReadAllLines("dane/pozycje.txt");
        return lines.Length;
    }

    private string BuildCountText(int count)
    {
        return $"Pozycji w pliku: {count}";
    }

    private void ShowList()
    {
        ScreenHost.Content = ListView;
    }

    private void ShowDetail()
    {
        PassSelectedToDetail();
        ScreenHost.Content = DetailView;
    }

    private void ShowAdd()
    {
        ScreenHost.Content = AddView;
    }

    private void PassSelectedToDetail()
    {
        var line = ListView.GetSelectedLine();
        if (line is not null)
        {
            DetailView.ShowLine(line);
        }
    }
}
