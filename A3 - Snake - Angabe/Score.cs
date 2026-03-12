using System.Globalization;

namespace A3___Snake___Angabe
{
    /// <summary>
    /// Class <c>Score</c> saves a score with points and time.
    /// </summary>
    internal class Score
    {
        private DateTime _date;
        private byte _points;

        public byte Points
        {
            get { return _points; }
            init { _points = value; }
        }
        /// <summary>
        /// If <paramref name="points"/> is 0 or not given, an empty score will be saved
        /// </summary>
        /// <param name="points"></param>
        public Score(byte points = 0)
        {
            Points = points;
            _date = DateTime.Now;
        }
        /// <summary>
        /// Method <c>ToString</c><br></br><br></br> Returns the current score as a string
        /// </summary>
        public override string ToString()
        {
            if (Points !=0)
                return $"{Points:d3};{_date.ToString("dd.MM.yyyy HH:mm:ss")}";
            return $"---;--.--.---- --:--:--";
        }
        /// <summary>
        /// Parses a span of UTF-8 characters into a value.<br/><br/>Accepted Format: points;dd.MM.yyyy HH:mm:ss
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Score Parse(string s)
        {
            if (s.IndexOf('-') != -1)
            {
                string[] splitS = s.Split(';');
                byte points = byte.Parse(splitS[0]);
                Score score = new Score(points);
                score._date = DateTime.ParseExact(splitS[1], format: "dd.MM.yyyy HH:mm:ss", CultureInfo.GetCultureInfo("de-AT"));
                return score;
            }
            else return new();
        }
    }
}
