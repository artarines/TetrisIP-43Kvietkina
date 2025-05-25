namespace TetrisWPF
{
    //ігрова сітка(клас зберігатиме двовимірний прямокутний масив(рядок і стовпчик))
    public class GameGrid
    {
        private readonly int[,] grid;
        public int Rows { get; }
        public int Columns { get; }
        public int this[int r, int c]
        {
            get => grid[r, c];
            set => grid[r, c] = value;
        }
        public GameGrid(int rows, int col)
        {
            Rows = rows;
            Columns = col;
            grid = new int[rows, col];
        }
        //перевірка чи знаходиться клітинка в середині сітки 
        public bool IsInside(int r, int c)
        {
            // щоб бути в середині:
            // рядок має бути >=0 та меншим за кількість рядків
            //стовпець має бути >=0 та меншим за кількість стовпців 
            return r >= 0 && r < Rows && c >= 0 && c < Columns;
        }
        // перевірка чи є задана комірка порожньою чи ні
        // (тобто значення клітинки має бути 0 якщо пуста та клітинка знаходитсь в межах сітки)
        public bool IsEmpty(int r, int c)
        {
            return IsInside(r, c) && grid[r, c] == 0;
        }
        // перевірка на заповненість рядку
        public bool IsRowFull(int r)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r, c] == 0)
                {
                    return false;
                }
            }
            return true;
        }
        // перевірка чи порожній рядок
        public bool IsRowEmpty(int r)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r, c] != 0)
                {
                    return false;
                }
            }
            return true;
        }
        // якщо ж повний ряд його треба звільнити, і на його місце перемістити той ряд, що був вижче
        // для очистки використаємо змінну з назвою Cleared яка буде містити кількість очищених рядків
        // метод для очищення рядків та метод що пересуває верхні рядки вниз на пусте місце після очищення
        private void ClearRow(int r)
        {
            for (int c = 0; c < Columns; c++)
            {
                grid[r, c] = 0;
            }
        }
        private void MoveRowDown(int r, int numRows)
        {
            for (int c = 0; c < Columns; c++)
            {
                grid[r + numRows, c] = grid[r, c];
                grid[r, c] = 0;
            }
        }

        public int ClearFullRows()
        {
            int cleared = 0;
            // перевірка чи поточний рядок заповнений(якщо заповнений - очищаємо його та очищаємо інкремент,
            // інакше переміщаємо верхні рядки на кількість рядків в змінній Cleared
            for (int r = Rows - 1; r >= 0; r--)
            {
                if (IsRowFull(r))
                {
                    ClearRow(r);
                    cleared++;
                }
                else if (cleared > 0)
                {
                    MoveRowDown(r, cleared);
                }
            }
            // 1 очко за кожну лінію + 5 бонусних очок за кожну додакову лінію 
            int score = cleared * 100;
            if (cleared == 2)
            {
                score += 100; // 300 - 200 = 100 бонус
            }
            else if (cleared == 3)
            {
                score += 400; // 700 - 300 = 400 бонус
            }
            else if (cleared == 4)
            {
                score += 1100; // 1500 - 400 = 1100 бонус
            }
            else if (cleared == 5)
            {
                score += 2000; // 2500 - 500 = 2000 бонус
            }
            return score;
        }
    }
}