namespace TetrisWPF
{
    public abstract class Block
    {
        protected abstract Position[][] Tiles { get; }
        protected abstract Position StartOffset { get; }
        public abstract int Id { get; }
        private int rotationState;
        private Position offset;
        public Block()
        {
            offset = new Position(StartOffset.Row, StartOffset.Column);
        }
        // напишемо метод який повертає позиції у сітці зайняті блоком з урахуванням поточного повороту та зсуву
        public IEnumerable<Position> TilePositions()
        {
            foreach (Position p in Tiles[rotationState])
            {
                yield return new Position(p.Row + offset.Row, p.Column + offset.Column);
            }
            // метод перебирає позиції плиток в поточному стані обертання та додає зсув рядка і стовпця 
        }

        // метод, що повертає фігурку на 90 градусів за год стрілкою,
        // робимо це інкрементуючи поточний стан обертання, обертаючи фігуру до нуля якщо фігура в кінцевому стані
        public void RotateCW()
        {
            rotationState = (rotationState + 1) % Tiles.Length;
        }
        // метод для обертання проти годинникової стрілки 
        public void RotateCCW()
        {
            if (rotationState == 0)
            {
                rotationState = Tiles.Length - 1;
            }
            else
            {
                rotationState--;
            }
        }
        // метод для руху фігури на задану кількість рядків і стовпців
        public void Move(int rows, int columns)
        {
            offset.Row += rows;
            offset.Column += columns;
        }
        // метод який скидає обертання і положення
        public void Reset()
        {
            rotationState = 0;
            offset.Row = StartOffset.Row;
            offset.Column = StartOffset.Column;
        }
    }
}
