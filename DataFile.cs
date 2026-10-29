using System;
using System.Collections.Generic;
using System.IO;

namespace katalog;

public static class DataFile
{
    private const string RelativePath = "dane/pozycje.txt";

    public static string FilePath
    {
        get { return ResolvePath(); }
    }

    public static List<string> ReadLines()
    {
        var lines = new List<string>();
        if (!File.Exists(FilePath))
        {
            return lines;
        }
        foreach (var line in File.ReadAllLines(FilePath))
        {
            AddIfNotEmpty(lines, line);
        }
        return lines;
    }

    public static int CountItems()
    {
        return ReadLines().Count;
    }

    public static void AppendLine(string line)
    {
        EnsureDirectory();
        EnsureTrailingNewline();
        File.AppendAllText(FilePath, line + "\n");
    }

    private static void AddIfNotEmpty(List<string> lines, string line)
    {
        if (!string.IsNullOrWhiteSpace(line))
        {
            lines.Add(line.Trim());
        }
    }

    private static string ResolvePath()
    {
        if (File.Exists(RelativePath))
        {
            return RelativePath;
        }
        return Path.Combine(AppContext.BaseDirectory, RelativePath);
    }

    private static void EnsureDirectory()
    {
        var directory = Path.GetDirectoryName(FilePath);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

    private static void EnsureTrailingNewline()
    {
        if (!File.Exists(FilePath))
        {
            return;
        }
        var text = File.ReadAllText(FilePath);
        if (text.Length > 0 && !text.EndsWith("\n"))
        {
            File.AppendAllText(FilePath, "\n");
        }
    }
}
