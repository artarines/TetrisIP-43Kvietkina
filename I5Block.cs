namespace TetrisWPF
{
    // збережемо позиції квадратів для чотирьох станів обертання
    public class I5Block : Block
    {
        private readonly Position[][] tiles = new Position[][]
        {
            new Position[] { new(0,2), new(1,2), new(2,2), new(3,2), new(4,2) },
            new Position[] { new(2,0), new(2,1), new(2,2), new(2,3), new(2,4) }
        };
        public override int Id => 9;
        protected override Position StartOffset => new Position(0, 2);//тоді фігура з'явиться в середині верхнього ряду
        protected override Position[][] Tiles => tiles;
    }
}