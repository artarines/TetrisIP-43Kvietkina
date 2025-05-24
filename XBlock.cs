namespace TetrisWPF
{
    // збережемо позиції квадратів для чотирьох станів обертання
    public class XBlock : Block
    {
        private readonly Position[][] tiles = new Position[][]
        {
            new Position[] { new(0,1), new(1,0), new(1,1), new(1,2), new(2,1) }
        };
        public override int Id => 17;
        protected override Position StartOffset => new Position(0, 3);//тоді фігура з'явиться в середині верхнього ряду
        protected override Position[][] Tiles => tiles;
    }
}
