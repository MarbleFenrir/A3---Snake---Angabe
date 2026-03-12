namespace A3___Snake___Angabe;
internal class Leaderboard
{
    private static string path = @"./highscore.txt";
    public Score[] scores = new Score[10];
    public Leaderboard()
    {
        if (!File.Exists(path))
            File.Create(path).Dispose();
        int scoreCount = 0;
        using (StreamReader sr = new StreamReader(path))
        {
            while (!sr.EndOfStream)
            {
                scores[scoreCount] = Score.Parse(sr.ReadLine());
                scoreCount++;
            }
        }
        for (int i = 0; i < scores.Length - scoreCount; i++)
            scores[i] = new Score();
    }
    public void Add()
    {
        ///
        /// TODO: Implement operations for easier use with "public static bool operator"
        /// 
    }
    public override string ToString()
    {
        string retval = "";
        for (int i = 0;i < scores.Length;i++)
        {
            retval += scores[i].ToString() + '\n';
        }
        return retval;
    }
}
