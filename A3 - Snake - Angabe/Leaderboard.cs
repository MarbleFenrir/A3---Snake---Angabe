namespace A3___Snake___Angabe;
internal class Leaderboard
{
    private static string PATH = @"./highscore.txt";
    public Score[] scores = new Score[10];
    public Leaderboard()
    {
        if (!File.Exists(PATH))
            File.Create(PATH).Dispose();
        int scoreCount = 0;
        using (StreamReader sr = new(PATH))
        {
            while (!sr.EndOfStream)
            {
                string? line = sr.ReadLine();
                if (line is null)
                    break;
                scores[scoreCount] = Score.Parse(line);
                scoreCount++;
            }
        }
        for (int i = 0; i < scores.Length - scoreCount; i++)
            scores[i] = new Score();
    }
    /// <summary>
    /// Adds a score to the appropriate place in the leaderboard
    /// </summary>
    /// <param name="s"></param>
    public void Add(Score s)
    {
        for (int i = 0; i < scores.Length; i++)
        {
            // If current slot is empty OR new score is higher
            if (scores[i] == null || s > scores[i])
            {
                // Shift everything down
                for (int j = scores.Length - 1; j > i; j--)
                {
                    scores[j] = scores[j - 1];
                }

                // Insert new score
                scores[i] = s;
                return;
            }
        }
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
    public void WriteFile()
    {
        try
        {
            string? directory = Path.GetDirectoryName(PATH);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (StreamWriter writer = new StreamWriter(PATH))
            {
                writer.Write(this.ToString());
            }

            Console.WriteLine($"Successfully wrote to file: {PATH}");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine($"Access denied to file: {PATH}");
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine($"Directory not found for path: {PATH}");
        }
        catch (PathTooLongException)
        {
            Console.WriteLine($"The file path is too long: {PATH}");
        }
        catch (IOException ioEx)
        {
            Console.WriteLine($"I/O error occurred while writing to file: {ioEx.Message}");
        }
    }
}
