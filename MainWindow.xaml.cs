using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TetrisWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ImageSource[] tileImages = new ImageSource[]
{
            new BitmapImage(new Uri("AssetsRes/TileEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/TileCyan.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/Blue111.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/TileOrange.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/TileYellow.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/TileGreen.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/TilePurple.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/Red.jpeg", UriKind.Relative))
};

        private readonly ImageSource[] blockImages = new ImageSource[]
        {
            new BitmapImage(new Uri("AssetsRes/Block-Empty.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/Block-I.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/Block-J.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/Block-L.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/Block-O.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/Block-S.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/Block-T.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/Block-Z.png", UriKind.Relative))
        };

        private readonly Image[,] imageControls;

        private GameState gameState = new GameState();
        public MainWindow()
        {
            InitializeComponent();
            imageControls = SetupGameCanvas(gameState.GameGrid);
        }

        private Image[,] SetupGameCanvas(GameGrid grid)
        {
            Image[,] imageControls = new Image[grid.Rows, grid.Columns];
            int cellSize = 25; //на кожну клітинку дає 25 пікселів
            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    Image imageControl = new Image()
                    {
                        Width = cellSize,
                        Height = cellSize,
                    };
                    //
                    Canvas.SetTop(imageControl, (r - 2) * cellSize);
                    Canvas.SetLeft(imageControl, c * cellSize);
                    GameCanvas.Children.Add(imageControl);
                    imageControls[r, c] = imageControl;
                }
            }
            return imageControls;
        }

        private void DrawGrid(GameGrid grid)
        {
            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    int id = grid[r, c];
                    imageControls[r, c].Source = tileImages[id];
                }
            }
        }

        private void DrawBlock(Block block)
        {
            foreach (Position p in block.TilePositions())
            {
                imageControls[p.Row, p.Column].Source = tileImages[block.Id];
            }
        }
        // виклик методу малювання сітки
        private void Draw(GameState gameState)
        {
            DrawGrid(gameState.GameGrid);
            DrawBlock(gameState.CurrentBlock);
        }
        
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameState.GameOver)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.Left:
                    gameState.MoveBlockLeft();
                    break;
                case Key.Right:
                    gameState.MoveBlockRight();
                    break;
                case Key.Down:
                    gameState.MoveBlockDown();
                    break;
                case Key.Q:
                    gameState.RotateBlockCW();
                    break;
                case Key.W:
                    gameState.RotateBlockCCW();
                    break;
                default:
                    return;
            }
            Draw(gameState);
        }

        private void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            Draw(gameState);
        }

        private void PlayAgain_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}