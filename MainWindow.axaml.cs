using Avalonia.Controls;
using Avalonia.Interactivity;

namespace katalog;

public partial class MainWindow : Window
{
    private readonly ListScreen ListScreenView = new();
    private readonly DetailScreen DetailScreenView = new();
    private readonly AddScreen AddScreenView = new();

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
        CountText.Text = BuildCountText(DataFile.CountItems());
    }

    private string BuildCountText(int count)
    {
        return $"Pozycji w pliku: {count}";
    }

    private void ShowList()
    {
        ListScreenView.Reload();
        ScreenHost.Content = ListScreenView;
    }

    private void ShowDetail()
    {
        DetailScreenView.Reload();
        PassSelectedToDetail();
        ScreenHost.Content = DetailScreenView;
    }

    private void ShowAdd()
    {
        ScreenHost.Content = AddScreenView;
    }

    private void PassSelectedToDetail()
    {
        var line = ListScreenView.GetSelectedLine();
        if (line is not null)
        {
            DetailScreenView.ShowLine(line);
        }
    }
}
