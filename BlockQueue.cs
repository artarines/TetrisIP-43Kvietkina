namespace TetrisWPF
{
    public class BlockQueue
    {
        private readonly Block[] tetraminoblocks = new Block[]
        {
            new IBlock(),
            new JBlock(),
            new LBlock(),
            new OBlock(),
            new SBlock(),
            new TBlock(),
            new ZBlock()
        };
        private readonly Block[] pentaminoblocks = new Block[]
        {
            //пентаміно
            new FBlock(),
            new I5Block(),
            new NBlock(),
            new PBlock(),
            new T5Block(),
            new UBlock(),
            new VBlock(),
            new WBlock(),
            new XBlock(),
            new YBlock(),
            new Z5Block()
        };

        private readonly Random random = new Random();
        private Block[] blocks;
        // властивість для наступного блоку в черзі
        public Block NextBlock { get; private set; }
        private GameMode currentMode;

        private int pentaminoCount = 0;
        public BlockQueue(GameMode mode)
        {
            SetGameMode(mode);
            NextBlock = RandomBlock();
        }

        public void SetGameMode(GameMode mode)
        {
            currentMode = mode;

            if (mode == GameMode.Tetramino)
                blocks = tetraminoblocks;
            else if (mode == GameMode.Pentamino)
                blocks = pentaminoblocks;
            else
                blocks = tetraminoblocks.Concat(pentaminoblocks).ToArray();
        }
        // метод який повертає рандомний блок
        private Block RandomBlock()
        {
            if (currentMode == GameMode.Pentamino)
            { 
                return pentaminoblocks[random.Next(pentaminoblocks.Length)];
            }
            else if(currentMode == GameMode.Tetramino)
            {
                return tetraminoblocks[random.Next(tetraminoblocks.Length)];
            }
            else
            {
                if (random.Next(2) == 0)
                {
                    return tetraminoblocks[random.Next(tetraminoblocks.Length)];
                }
                else
                {
                    return pentaminoblocks[random.Next(pentaminoblocks.Length)];
                }
            }
        }

        public Block GetAndUpdate()
        {
            Block block = NextBlock;
            NextBlock = RandomBlock();
            return block;
        }
    }
}