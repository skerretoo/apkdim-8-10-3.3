using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace katalog;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ShowList();
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
        ScreenHost.Content = new ListScreen();
    }
}
