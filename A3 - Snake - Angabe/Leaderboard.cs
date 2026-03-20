namespace A3___Snake___Angabe;
internal class Leaderboard
{
    private static string path = @"./highscore.txt";
    public Score[] scores = new Score[10];
    public Leaderboard()
    {
        if (!File.Exists(path))
        {
            using (StreamWriter sw = new StreamWriter(path)) { }
        }

        int scoreCount = 0;

        using (StreamReader sr = new StreamReader(path))
        {
            while (!sr.EndOfStream && scoreCount < scores.Length)
            {
                string line = sr.ReadLine();
                if (line != null && line != "") 
                {
                    scores[scoreCount] = Score.Parse(line);
                    scoreCount++;
                }
            }
        }

        for (int i = scoreCount; i < scores.Length; i++)
        {
            scores[i] = new Score();
        }
    }
    /// <summary>
    /// Adds a score to the appropriate place in the leaderboard
    /// </summary>
    /// <param name="s"></param>
    public void Add(Score s)
    {
        scores[scores.Length - 1] = s;

        for (int i = 0; i < scores.Length - 1; i++)
        {
            for (int j = 0; j < scores.Length - 1 - i; j++)
            {
                if (scores[j].Points < scores[j + 1].Points)
                {
                    Score temp = scores[j];
                    scores[j] = scores[j + 1];
                    scores[j + 1] = temp;
                }
            }
        }

        Save();
    }
    private void Save()
    {
        using (StreamWriter sw = new StreamWriter(path))
        {
            for (int i = 0; i < scores.Length; i++)
            {
                sw.WriteLine(scores[i].ToString());
            }
        }
    }
    public override string ToString()
    {
        string retval = "";
        for (int i = 0;i < scores.Length - 1;i++)
        {
            retval += scores[i].ToString() + '\n';
        }
        return retval;
    }
}
