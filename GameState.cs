namespace TetrisWPF
{
    public enum GameMode { Tetromino, Pentomino, Mixed }
    public class GameState
    {
        private Block currentBlock;
        public Block CurrentBlock
        {
            get => currentBlock;
            private set
            {
                currentBlock = value;
                currentBlock.Reset();
                
                for (int i = 0; i < 2; i++)
                {
                    currentBlock.Move(1, 0);
                    if (!BlockFits())
                    {
                        currentBlock.Move(-1, 0);
                    }

                }
            }
        }

        public GameGrid GameGrid { get; private set; }
        public BlockQueue BlockQueue { get; private set;}
        public bool GameOver { get; private set; }
        public int Score { get; private set; }
        public int Level { get; private set; } = 0;
        public GameMode CurrentMode { get; private set; } = GameMode.Tetromino; //початковий режим
        public Block  HeldBlock { get; private set; }
        public bool CanHold { get; private set; }

        public GameState()
        {
            GameGrid = new GameGrid(22, 10);
            BlockQueue = new BlockQueue();
            CurrentBlock = BlockQueue.GetAndUpdate();
            CanHold = true;
        }

        public void SetGameMode(GameMode mode)
        {
            CurrentMode = mode;
            BlockQueue.SetGameMode(mode); // Оновлюємо чергу блоків для режиму
        }

        public double GetTimerInterval()
        {
            return 1000.0 / (Level + 1); // 1000 мс на Level 0, 500 мс на Level 1 тощо
        }
        // метод щоб знайти чи знаходиться блок в легальній позиції чи ні
        // цей метод перевіряє позиції плиток поточної фігури, якщо якась клітинка знаходиться поза сіткою
        // або перекривається іншою фігурою, вертаємо 0, інакше 1
        private bool BlockFits()
        {
            foreach (Position p in CurrentBlock.TilePositions()) 
            {
                if (!GameGrid.IsEmpty(p.Row, p.Column))
                {
                    return false;
                }
            }
            return true;
        }

        public void HoldBlock()
        {
            if (!CanHold)
            {
                return;
            }

            if (HeldBlock == null)
            {
                HeldBlock = CurrentBlock;
                CurrentBlock = BlockQueue.GetAndUpdate();
            }
            else
            {
                Block tmp = CurrentBlock;
                CurrentBlock = HeldBlock;
                HeldBlock = tmp;
            }
            CanHold = false;
        }
        // поворот блоку за год стрілкою, з того місця де він є враховуючи обмеження сітки
        public void RotateBlockCW()
        {
            CurrentBlock.RotateCW();
            if (!BlockFits())
            {
                CurrentBlock.RotateCCW();
            }
        }
        // проти год стрілки
        public void RotateBlockCCW()
        {
            CurrentBlock.RotateCCW();
            if (!BlockFits())
            {
                CurrentBlock.RotateCW();
            }
        }

        // рухи вліво/вправо
        public void MoveBlockLeft()
        {
            CurrentBlock.Move(0, -1);
            if (!BlockFits())
            {
                CurrentBlock.Move(0, 1);
            }
        }
        public void MoveBlockRight()
        {
            CurrentBlock.Move(0, 1);
            if (!BlockFits())
            {
                CurrentBlock.Move(0, -1);
            }
        }

        // перевірка чи закінчилась гра
        private bool IsGameOver()
        {
            return !(GameGrid.IsRowEmpty(0) && GameGrid.IsRowEmpty(1));
        }

        private void UpdateLevel()
        {
            if (Score >= 300) Level = 10;
            else if (Score >= 150) Level = 9;
            else if (Score >= 120) Level = 8;
            else if (Score >= 90) Level = 7;
            else if (Score >= 80) Level = 6;
            else if (Score >= 70) Level = 5;
            else if (Score >= 55) Level = 4;
            else if (Score >= 40) Level = 3;
            else if (Score >= 25) Level = 2;
            else if (Score >= 10) Level = 1;
            else Level = 0;
        }

        // перевіряємо позиції в сітці відповідно до ідентифікторів фігур
        // очистка всіх потенційних рядків і перевірка чи завершилася гра
        private void PlaceBlock()
        {
            foreach (Position p in CurrentBlock.TilePositions())
            {
                GameGrid[p.Row, p.Column] = CurrentBlock.Id;
            }
            
            Score += GameGrid.ClearFullRows();
            UpdateLevel();

            if (IsGameOver())
            {
                GameOver = true;
            }
            else
            {
                CurrentBlock = BlockQueue.GetAndUpdate();
                CanHold = true;
            }
        }
        public void MoveBlockDown()
        {
            CurrentBlock.Move(1, 0);
            if (!BlockFits())
            {
                CurrentBlock.Move(-1, 0);
                PlaceBlock();
            }
        }

        private int TileDropDistance(Position p)
        {
            int drop = 0;

            while (GameGrid.IsEmpty(p.Row + drop + 1, p.Column))
            {
                drop++;
            }
            return drop;
        }

        public int BlockDropDistance()
        {
            int drop = GameGrid.Rows;

            foreach (Position p in CurrentBlock.TilePositions())
            {
                drop = System.Math.Min(drop, TileDropDistance(p));
            }
            return drop;
        }

        public void DropBlock()
        {
            CurrentBlock.Move(BlockDropDistance(), 0);
            PlaceBlock();
        }
    }
}
