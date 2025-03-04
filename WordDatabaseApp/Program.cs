class Program
{
    static void Main(string[] args)
    {
        var wordDatabase = new WordDatabase();

        // Ścieżka do pliku z bazą danych (z użyciem podwójnych ukośników lub @)
        string inputFilePath = @"C:\Users\pawel\Desktop\DuplicatesEraser\WordDatabaseApp\word_database.txt";  // Możesz zaktualizować tę ścieżkę
        string outputFilePath = @"C:\Users\pawel\Desktop\DuplicatesEraser\WordDatabaseApp\word_database_processed.txt";  // Możesz zaktualizować tę ścieżkę

        // Wczytanie bazy słów z pliku
        wordDatabase.LoadDatabase(inputFilePath);

        // Wyświetlanie bazy słów
        wordDatabase.DisplayWords();

        // Zapisanie bazy do nowego pliku (po usunięciu duplikatów)
        wordDatabase.SaveDatabase(outputFilePath);

        Console.WriteLine("Proces zakończony.");
    }
}
