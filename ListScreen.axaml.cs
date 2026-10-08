using System.Collections.Generic;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace katalog;

public partial class ListScreen : UserControl
{
    public ListScreen()
    {
        InitializeComponent();
        LoadItems();
    }

    public string? GetSelectedLine()
    {
        return ItemsList.SelectedItem as string;
    }

    private void OnRefreshClick(object? sender, RoutedEventArgs e)
    {
        LoadItems();
    }

    private void LoadItems()
    {
        ShowLines(ReadLines());
    }

    private List<string> ReadLines()
    {
        return new List<string>(File.ReadAllLines("dane/pozycje.txt"));
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
        StatusText.Text = $"Wczytano pozycji: {count}";
    }
}
