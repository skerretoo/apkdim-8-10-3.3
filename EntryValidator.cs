namespace katalog;

public static class EntryValidator
{
    private const int MinYear = 1800;
    private const int MaxYear = 2100;
    private const int MinRating = 1;
    private const int MaxRating = 10;

    public static string? Validate(string title, string year, string rating)
    {
        if (title.Length == 0 || title.Contains(';'))
        {
            return "Uzupełnij tytuł. W tytule nie używaj średnika.";
        }
        if (!IsNumberInRange(year, MinYear, MaxYear))
        {
            return "Rok wydania musi być liczbą 1800–2100.";
        }
        if (!IsNumberInRange(rating, MinRating, MaxRating))
        {
            return "Ocena musi być liczbą 1–10.";
        }
        return null;
    }

    private static bool IsNumberInRange(string value, int min, int max)
    {
        return int.TryParse(value, out var number) && number >= min && number <= max;
    }
}
