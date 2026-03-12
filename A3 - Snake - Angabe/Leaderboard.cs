namespace A3___Snake___Angabe;
internal class Leaderboard
{
    private static string path = @"./highscore.txt";
    public Score[] scores;
    public Leaderboard()
    {
        if (!File.Exists(path))
            File.Create(path).Dispose();
        int scoreCount = 0;
        using (StreamReader sr = new StreamReader(path))
        {
            while (!sr.EndOfStream)
            {
                scoreCount++;

            }
        }
    }
}
