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

for (int i = scoreCount; i < scores.Length; i++)
    scores[i] = new Score();
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
            // If current slot is empty OR new score is higher
            if (scores[i] == null || scores[i].Points == 0 || s > scores[i])
            {
                if (scores[j].Points < scores[j + 1].Points)
                {
                    Score temp = scores[j];
                    scores[j] = scores[j + 1];
                    scores[j + 1] = temp;
                }
            }
        }

        WriteFile();
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
