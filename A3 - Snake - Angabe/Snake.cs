namespace A3___Snake___Angabe
{
    internal static class Snake
    {
        private static int length = 3;
        private static int _headX = 5;
        private static int _headY = 5;
        public static void MoveLeft()
        {
            if (_headX < 39)
                _headX++;
        }
        public static void MoveRight()
        {
            if ( _headX > 0) 
                _headX--;
        }
        public static void MoveUp()
        {
            if (_headY < 19)
                _headY++;
        }
        public static void MoveDown()
        {
            if (_headY > 0)
                _headY--;
        }
    }
}
