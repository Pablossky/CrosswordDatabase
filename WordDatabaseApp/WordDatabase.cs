using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

public class WordDatabase
{
    public Dictionary<string, string> Words { get; private set; }

    public WordDatabase()
    {
        Words = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    }
    public void LoadDatabase(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Plik bazy danych nie istnieje.");
            return;
        }

        var lines = File.ReadAllLines(filePath);

        //format { "HASŁO", "PODPOWIEDŹ" }
        var regex = new Regex(@"{\s*""([^""]+)""\s*,\s*""([^""]+)""\s*}");

        foreach (var line in lines)
        {
            var match = regex.Match(line);
            if (match.Success)
            {
                string word = match.Groups[1].Value.Trim();
                string clue = match.Groups[2].Value.Trim();

                if (!Words.ContainsKey(word))
                {
                    Words[word] = clue;
                }
            }
        }

        Console.WriteLine("Baza słów załadowana.");
    }

    public void SaveDatabase(string filePath)
{
    var lines = new List<string>();

    var lastIndex = Words.Count - 1;

    foreach (var pair in Words.Select((pair, index) => new { pair, index }))
    {
        string line = $"{{ \"{pair.pair.Key}\", \"{pair.pair.Value}\" }}";
        if (pair.index != lastIndex) 
        {
            line += ",";  
        }
        lines.Add(line);
    }

    File.WriteAllLines(filePath, lines);
    Console.WriteLine("Baza słów zapisana.");
}

    public void DisplayWords()
    {
        Console.WriteLine("Baza słów:");
        foreach (var pair in Words)
        {
            Console.WriteLine($"Hasło: {pair.Key}, Podpowiedź: {pair.Value}");
        }
    }
}

