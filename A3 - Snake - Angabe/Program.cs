namespace A3___Snake___Angabe;

internal class Program
{
    public static int seed = (int)(DateTime.Now.Ticks % int.MaxValue);
    static int Main(string[] args)
    {
        #region Args
        if (args.Length <= 2)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-s" || args[i] == "--seed")
                {
                    if (int.TryParse(args[++i], out seed))
                    {
                        break;
                    }
                    else
                    {
                        Console.Error.WriteLine("Not a valid seed.");
                        Environment.Exit(1);
                    }
                }
            }
        }
        #endregion Args
        Game game = new(seed);
        while (game.Start()) 
        {
            game = new((int)Math.Floor(seed + Math.Atan(game.score)));
        }
        return 0;
    }
}
