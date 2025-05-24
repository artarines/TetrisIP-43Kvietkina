using System;

namespace TetrisWPF
{
    public class BlockQueue
    {
        private readonly Block[] tetrominoblocks = new Block[]
        {
            new IBlock(),
            new JBlock(),
            new LBlock(),
            new OBlock(),
            new SBlock(),
            new TBlock(),
            new ZBlock()
        };
        private readonly Block[] pentominoblocks = new Block[]
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
        public BlockQueue()
        {
            SetGameMode(GameMode.Tetromino);
            NextBlock = RandomBlock();
        }

        public void SetGameMode(GameMode mode)
        {
            currentMode = mode;
            if (mode == GameMode.Tetromino)
                blocks = tetrominoblocks;
            else if (mode == GameMode.Pentomino)
                blocks = pentominoblocks;
            else
                blocks = tetrominoblocks.Concat(pentominoblocks).ToArray();
        }
        // метод який повертає рандомний блок
        private Block RandomBlock()
        {
            return blocks[random.Next(blocks.Length)];
        }

        public Block GetAndUpdate()
        {
            Block block = NextBlock;
            NextBlock = RandomBlock();
            return block;
        }
    }
}