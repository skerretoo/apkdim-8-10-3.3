using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace katalog;

public partial class AddScreen : UserControl
{
    public AddScreen()
    {
        InitializeComponent();
    }

    private void OnSaveClick(object? sender, RoutedEventArgs e)
    {
        TrySave();
    }

    private void TrySave()
    {
        var title = ReadTitle();
        var year = ReadYear();
        var rating = ReadRating();
        if (!IsValid(title, year, rating))
        {
            ShowError("Uzupełnij tytuł, rok i ocenę. W tytule nie używaj średnika.");
            return;
        }
        AppendLine(BuildLine(title, year, rating));
        ClearInputs();
        ShowSuccess("Zapisano do dane/pozycje.txt");
    }

    private string ReadTitle()
    {
        return TitleBox.Text?.Trim() ?? "";
    }

    private string ReadYear()
    {
        return YearBox.Text?.Trim() ?? "";
    }

    private string ReadRating()
    {
        return RatingBox.Text?.Trim() ?? "";
    }

    private bool IsValid(string title, string year, string rating)
    {
        return IsTitleValid(title) && IsYearValid(year) && IsRatingValid(rating);
    }

    private bool IsTitleValid(string title)
    {
        return title.Length > 0 && !title.Contains(';');
    }

    private bool IsYearValid(string year)
    {
        return int.TryParse(year, out var number) && number >= 1800 && number <= 2100;
    }

    private bool IsRatingValid(string rating)
    {
        return rating.Length > 0 && !rating.Contains(';');
    }

    private string BuildLine(string title, string year, string rating)
    {
        return $"{title};{year};{rating}";
    }

    private void AppendLine(string line)
    {
        File.AppendAllText("dane/pozycje.txt", line + "\n");
    }

    private void ClearInputs()
    {
        TitleBox.Text = "";
        YearBox.Text = "";
        RatingBox.Text = "";
    }

    private void ShowError(string message)
    {
        MessageText.Text = message;
    }

    private void ShowSuccess(string message)
    {
        MessageText.Text = message;
    }
}
