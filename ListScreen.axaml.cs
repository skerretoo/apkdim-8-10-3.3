using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace katalog;

public partial class ListScreen : UserControl
{
    public ListScreen()
    {
        InitializeComponent();
        Reload();
    }

    public void Reload()
    {
        ShowLines(DataFile.ReadLines());
    }

    public string? GetSelectedLine()
    {
        return ItemsList.SelectedItem as string;
    }

    private void OnRefreshClick(object? sender, RoutedEventArgs e)
    {
        Reload();
    }

    private void ShowLines(List<string> lines)
    {
        ItemsList.Items.Clear();
        AddLines(lines);
        ShowCount(lines.Count);
    }

    private void AddLines(List<string> lines)
    {
        foreach (var line in lines)
        {
            ItemsList.Items.Add(line);
        }
    }

    private void ShowCount(int count)
    {
        if (count == 0)
        {
            StatusText.Text = "Brak pozycji. Dodaj pierwszą na ekranie Dodaj.";
            return;
        }
        StatusText.Text = $"Wczytano pozycji: {count}";
    }
}
