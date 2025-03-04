using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

public class WordDatabase
{
    public Dictionary<string, (string Clue, string Difficulty)> Words { get; private set; }

    public WordDatabase()
    {
        Words = new Dictionary<string, (string Clue, string Difficulty)>(StringComparer.OrdinalIgnoreCase);
    }

    public void LoadDatabase(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Plik bazy danych nie istnieje.");
            return;
        }

        var lines = File.ReadAllLines(filePath);

        // Format: "WORD", "CLUE", "HARD" (optional)
        var regex = new Regex(@"\s*""([^""]+)""\s*,\s*""([^""]+)""\s*(,\s*""([^""]+)"")?\s*");

        foreach (var line in lines)
        {
            var match = regex.Match(line);
            if (match.Success)
            {
                string word = match.Groups[1].Value.Trim();
                string clue = match.Groups[2].Value.Trim();
                string difficulty = match.Groups[4].Success ? match.Groups[4].Value.Trim() : string.Empty;

                Words[word] = (clue, difficulty);
            }
        }

        Console.WriteLine("Baza słów załadowana.");
    }

    public void SaveDatabase(string filePath)
    {
        // Ensure the directory exists before attempting to save
        var directory = Path.GetDirectoryName(filePath);
        var lines = new List<string>();

        foreach (var pair in Words)
        {
            if (string.IsNullOrEmpty(pair.Value.Difficulty))
            {
                // Save words in the format: AddWord("WORD", "CLUE");
                lines.Add($"AddWord(\"{pair.Key}\", \"{pair.Value.Clue}\");");
            }
            else
            {
                // Save words with difficulty in the format: AddWord("WORD", "CLUE", "HARD");
                lines.Add($"AddWord(\"{pair.Key}\", \"{pair.Value.Clue}\", \"{pair.Value.Difficulty}\");");
            }
        }

        // Write the lines to the file
        File.WriteAllLines(filePath, lines);
        Console.WriteLine("Baza słów zapisana.");
    }

    public void DisplayWords()
    {
        Console.WriteLine("Baza słów:");
        foreach (var pair in Words)
        {
            string difficultyText = string.IsNullOrEmpty(pair.Value.Difficulty) ? "" : $", {pair.Value.Difficulty}";
            Console.WriteLine($"Hasło: {pair.Key}, Podpowiedź: {pair.Value.Clue}{difficultyText}");
        }
    }
}
