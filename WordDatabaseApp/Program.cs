public class Program
{
    public static void Main(string[] args)
    {
        WordDatabase db = new WordDatabase();

        string loadFilePath = @"C:\Users\pawel\Desktop\Words\CrosswordDatabase\WordDatabaseApp\word_database.txt";
        string saveFilePath = @"C:\Users\pawel\Desktop\Words\CrosswordDatabase\WordDatabaseApp\word_database_process.txt";

        db.LoadDatabase(loadFilePath);
        db.DisplayWords();
        db.SaveDatabase(saveFilePath);
    }
}
