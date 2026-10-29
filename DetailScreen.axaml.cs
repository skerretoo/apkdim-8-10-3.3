using System.Collections.Generic;
using Avalonia.Controls;

namespace katalog;

public partial class DetailScreen : UserControl
{
    public DetailScreen()
    {
        InitializeComponent();
        Reload();
    }

    public void Reload()
    {
        var current = PickerBox.SelectedItem as string;
        var lines = DataFile.ReadLines();
        FillPicker(lines);
        SelectLine(lines, current);
    }

    public void ShowLine(string line)
    {
        ShowParts(SplitLine(line));
    }

    private void OnPickerChanged(object? sender, SelectionChangedEventArgs e)
    {
        ShowSelected();
    }

    private void FillPicker(List<string> lines)
    {
        PickerBox.Items.Clear();
        foreach (var line in lines)
        {
            PickerBox.Items.Add(line);
        }
    }

    private void SelectLine(List<string> lines, string? wanted)
    {
        var index = wanted is null ? -1 : lines.IndexOf(wanted);
        if (index < 0)
        {
            index = lines.Count > 0 ? 0 : -1;
        }
        if (index < 0)
        {
            ClearFields();
            ShowError("Brak pozycji w pliku dane/pozycje.txt.");
            return;
        }
        PickerBox.SelectedIndex = index;
    }

    private void ShowSelected()
    {
        var line = PickerBox.SelectedItem as string;
        if (line is null)
        {
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

    private void ClearFields()
    {
        TitleText.Text = "—";
        YearText.Text = "—";
        RatingText.Text = "—";
    }

    private void ShowError(string message)
    {
        ErrorText.Text = message;
    }
}
