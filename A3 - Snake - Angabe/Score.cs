namespace A3___Snake___Angabe
{
    /// <summary>
    /// Class <c>Score</c> saves a score with Name and points.
    /// </summary>
    internal class Score
    {
        public string Name { get; set; }
        private DateTime _date;
        private byte _points;

        public byte Points
        {
            get { return _points; }
            init { _points = value; }
        }
        /// <summary>
        /// If <paramref name="name"/> is null or not given, the screen will be cleared
        /// </summary>
        /// <param name="points"></param>
        /// <param name="name"></param>
        public Score(byte points = 0, string name = null)
        {
            Points = points;
            _date = DateTime.Now;
            if (name != null) Name = name;
            else
            {
                throw new NotImplementedException();
                Console.Clear();
                Console.WriteLine("Please enter your Name: ");
                Console.ReadLine();
            }
        }
        /// <summary>
        /// Method <c>ToString</c><br></br><br></br> Returns the current score as a string
        /// </summary>
        public override string ToString()
        {
            return $"{null:f2} {Name:f15} {_date.Date}; {_date.TimeOfDay}";
        }
    }
}
