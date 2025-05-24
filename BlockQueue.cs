using System;

namespace TetrisWPF
{
    public class BlockQueue
    {
        private readonly Block[] blocks = new Block[]
        {
            new IBlock(),
            new JBlock(),
            new LBlock(),
            new OBlock(),
            new SBlock(),
            new TBlock(),
            new ZBlock(),
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
        // властивість для наступного блоку в черзі
        public Block NextBlock { get; private set; }
        
        public BlockQueue()
        {
            NextBlock = RandomBlock();
        }
        // метод який повертає рандомний блок
        private Block RandomBlock()
        {
            return blocks[random.Next(blocks.Length)];
        }

        public Block GetAndUpdate()
        {
            Block block = NextBlock;
            do
            {
                NextBlock = RandomBlock();
            }
            while (block.Id == NextBlock.Id);
            
            return block;
        }
    }
}
