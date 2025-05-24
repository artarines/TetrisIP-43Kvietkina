namespace TetrisWPF
{
    // збережемо позиції квадратів для чотирьох станів обертання
    public class NBlock : Block
    {
        private readonly Position[][] tiles = new Position[][]
        {
            new Position[] { new(1,3), new(2,2), new(2,3), new(3,2), new(4,2) },
            new Position[] { new(2,0), new(2,1), new(2,2), new(3,2), new(3,3) },
            new Position[] { new(0,2), new(1,2), new(2,2), new(2,1), new(3,1) },
            new Position[] { new(1,1), new(1,2), new(2,2), new(2,3), new(2,4) }
        };
        public override int Id => 10;
        protected override Position StartOffset => new Position(0, 3);//тоді фігура з'явиться в середині верхнього ряду
        protected override Position[][] Tiles => tiles;
    }
}
