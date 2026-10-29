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
        var error = EntryValidator.Validate(title, year, rating);
        if (error is not null)
        {
            ShowMessage(error);
            return;
        }
        AppendEntry(title, year, rating);
        ClearInputs();
        ShowMessage("Zapisano do dane/pozycje.txt");
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

    private void AppendEntry(string title, string year, string rating)
    {
        DataFile.AppendLine(BuildLine(title, year, rating));
    }

    private string BuildLine(string title, string year, string rating)
    {
        return $"{title};{year};{rating}";
    }

    private void ClearInputs()
    {
        TitleBox.Text = "";
        YearBox.Text = "";
        RatingBox.Text = "";
    }

    private void ShowMessage(string message)
    {
        MessageText.Text = message;
    }
}
