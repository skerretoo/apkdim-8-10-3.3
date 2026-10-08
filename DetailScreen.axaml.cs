using System.Collections.Generic;
using System.IO;
using Avalonia.Controls;

namespace katalog;

public partial class DetailScreen : UserControl
{
    public DetailScreen()
    {
        InitializeComponent();
        LoadPicker();
    }

    public void ShowLine(string line)
    {
        ShowParts(SplitLine(line));
    }

    private void OnPickerChanged(object? sender, SelectionChangedEventArgs e)
    {
        ShowSelected();
    }

    private void LoadPicker()
    {
        FillPicker(ReadLines());
        SelectFirst();
    }

    private List<string> ReadLines()
    {
        return new List<string>(File.ReadAllLines("dane/pozycje.txt"));
    }

    private void FillPicker(List<string> lines)
    {
        PickerBox.Items.Clear();
        foreach (var line in lines)
        {
            PickerBox.Items.Add(line);
        }
    }

    private void SelectFirst()
    {
        if (PickerBox.Items.Count > 0)
        {
            PickerBox.SelectedIndex = 0;
        }
    }

    private void ShowSelected()
    {
        var line = PickerBox.SelectedItem as string;
        if (line is null)
        {
            ShowError("Wybierz pozycję z listy.");
            return;
        }
        ShowLine(line);
    }

    private string[] SplitLine(string line)
    {
        return line.Split(';');
    }

    private void ShowParts(string[] parts)
    {
        if (parts.Length != 3)
        {
            ShowError("Zły format linii. Oczekiwano: tytuł;rok;ocena");
            return;
        }
        ShowFields(parts[0], parts[1], parts[2]);
    }

    private void ShowFields(string title, string year, string rating)
    {
        TitleText.Text = title;
        YearText.Text = year;
        RatingText.Text = rating;
        ErrorText.Text = "";
    }

    private void ShowError(string message)
    {
        ErrorText.Text = message;
    }
}
