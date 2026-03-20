using System.Globalization;

namespace A3___Snake___Angabe;

/// <summary>
/// Class <c>Score</c> saves a score with points and time.
/// </summary>
internal class Score
{
    #region Properties
    private string name;

    public string Name
    {
        get { return name; }
        init { name = value; }
    }

    private DateTime _date;
    private ushort _points;

    public ushort Points
    {
        get { return _points; }
        init { _points = value; }
    }
    #endregion Properties
    #region Constructors
    /// <summary>
    /// If <paramref name="points"/> is 0 or not given, an empty score will be saved
    /// </summary>
    /// <param name="points"></param>
    public Score(ushort points = 0) : this("", points) { }
    public Score(string name, ushort points) : this(name, DateTime.Now, points) { }
    public Score(DateTime date, ushort points) : this("", date, points) { }
    public Score (string name, DateTime date, ushort points)
    {
        Name = name;
        Points=points;
        _date = date;
    }

    #endregion Constructors
    #region Methods
    /// <summary>
    /// Method <c>ToString</c><br></br><br></br> Returns the current score as a string
    /// </summary>
    public override string ToString()
    {
        if (Points !=0)
        {
            if (name == default)
                return $"{Points:d3};{new string('-', 15)};{_date.ToString("dd.MM.yyyy HH:mm:ss")}";
            else
                return $"{Points};{Name};{_date.ToString("dd.MM.yyyy HH:mm:ss")}";

        }
        return $"---;------------;--.--.---- --:--:--";
    }
    /// <summary>
    /// Parses a span of UTF-8 characters into a value.<br/><br/>Accepted Format: points;dd.MM.yyyy HH:mm:ss
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static Score Parse(string s)
    {
        if (s.IndexOf('-') == -1)
        {
            string[] splitS = s.Split(';');
            ushort points = ushort.Parse(splitS[0]);
            Score score = new Score(splitS[1], DateTime.ParseExact(splitS[2], format: "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture), points);
            return score;
        }
        else return new();
    }
    #endregion Methods
    #region Operators
    public static bool operator <(Score left, Score right)
    {
        return left.Points < right.Points;
    }
    public static bool operator >(Score left, Score right)
    {
        return left.Points > right.Points;
    }
    #endregion Operators
}
