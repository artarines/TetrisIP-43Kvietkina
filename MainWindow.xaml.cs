using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TetrisWPF
{
    // Interaction logic for MainWindow.xaml
    public partial class MainWindow : Window
    {
        private readonly ImageSource[] tileImages = new ImageSource[]
        {
            new BitmapImage(new Uri("AssetsRes/TileEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/YellowTile.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/PurpleTile.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/OrangeTile.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/PinkTile.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/BlueTile.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/CyanTile.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/GreenTile.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/PinkTile.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/YellowTile.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/GreenTile.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/YellowTile.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/CyanTile.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/GreenTile.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/OrangeTile.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/BlueTile.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/PinkTile.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/BlueTile.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/PurpleTile.png", UriKind.Relative))
        }; 

        private readonly ImageSource[] blockImages = new ImageSource[]
        {
            new BitmapImage(new Uri("AssetsRes/EmptyBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/IBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/JBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/LBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/OBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/SBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/TBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/ZBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/FBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/I5Block.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/NBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/PBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/T5Block.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/UBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/VBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/WBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/XBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/YBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsRes/Z5Block.png", UriKind.Relative))
        };

        private readonly Image[,] imageControls;
        private readonly int maxDelay = 1000;
        private readonly int minDelay = 75;
        private readonly int delayDecrease = 25;
        private int previousLevel = -1;

        private GameState gameState = new GameState();
        private GameMode currentGameMode;
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
                    Canvas.SetTop(imageControl, (r - 2) * cellSize + 10);
                    Canvas.SetLeft(imageControl, c * cellSize);
                    GameCanvas.Children.Add(imageControl);
                    imageControls[r, c] = imageControl;
                }
            }
            return imageControls;
        }

        private void UpdateState()
        {
            Draw(gameState);
            LevelText.Text = $"Level: {gameState.Level}";
            ScoreText.Text = $"Score: {gameState.Score}";
        }

        private void DrawGrid(GameGrid grid)
        {
            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    int id = grid[r, c];
                    imageControls[r, c].Opacity = 1;
                    imageControls[r, c].Source = tileImages[id];
                }
            }
        }

        private void DrawBlock(Block block)
        {
            foreach (Position p in block.TilePositions())
            {
                imageControls[p.Row, p.Column].Opacity = 1;
                imageControls[p.Row, p.Column].Source = tileImages[block.Id];
            }
        }

        private void DrawNextBlock(BlockQueue blockQueue)
        {
            Block next = blockQueue.NextBlock;
            NextImage.Source = blockImages[next.Id];
        }

        private void DrawHeldBlock(Block heldBlock)
        {
            if (heldBlock == null)
            {
                HoldImage.Source = blockImages[0];
            }
            else
            {
                HoldImage.Source = blockImages[heldBlock.Id];
            }
        }

        private void DrawGhostBlock(Block block)
        {
            int dropDistance = gameState.BlockDropDistance();

            foreach (Position p in block.TilePositions())
            {
                imageControls[p.Row + dropDistance, p.Column].Opacity = 0.25;
                imageControls[p.Row + dropDistance, p.Column].Source = tileImages[block.Id];
            }
        }
        // виклик методу малювання сітки
        private void Draw(GameState gameState)
        {
            DrawGrid(gameState.GameGrid);
            DrawGhostBlock(gameState.CurrentBlock);
            DrawBlock(gameState.CurrentBlock);
            DrawNextBlock(gameState.BlockQueue);
            DrawHeldBlock(gameState.HeldBlock);
            LevelText.Text = $"Level: {gameState.Level}";
            ScoreText.Text = $"Score: {gameState.Score}";
        }

        // метод ігрового циклу
        private async Task GameLoop()
        {
            Draw(gameState);

            while (!gameState.GameOver)
            {
                int delay = (int)gameState.GetTimerInterval();
                await Task.Delay(delay);
                gameState.MoveBlockDown();
                Draw(gameState);
            }
            GameOverMenu.Visibility = Visibility.Visible;
            FinalScoreText.Text = $"Score: {gameState.Score}";
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
                case Key.E:
                    gameState.HoldBlock();
                    break;
                default:
                    return;
            }
            Draw(gameState);
        }

        private async void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            await GameLoop();
        }

        private async void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            gameState = new GameState();
            GameOverMenu.Visibility = Visibility.Hidden;
            previousLevel = -1;
            GameTypeTextBlock.Text = $"Game Type: {currentGameMode}";
            await GameLoop();
        }
        private async void MixedMode_Click(object sender, RoutedEventArgs e)
        {
            gameState = new GameState();
            gameState.SetGameMode(GameMode.Mixed); // Припускаємо метод для режиму
            GameModeMenu.Visibility = Visibility.Hidden;
            GameTypeTextBlock.Text = "Game mode: Mixed";
            previousLevel = -1;
            await GameLoop();
        }

        private async void TetraminoMode_Click(object sender, RoutedEventArgs e)
        {
            gameState = new GameState();
            gameState.SetGameMode(GameMode.Tetramino);
            GameModeMenu.Visibility = Visibility.Hidden;
            GameTypeTextBlock.Text = "Game mode: Tetramino";
            previousLevel = -1;
            await GameLoop();
        }

        private async void PentaminoMode_Click(object sender, RoutedEventArgs e)
        {
            gameState = new GameState();
            gameState.SetGameMode(GameMode.Pentamino);
            GameModeMenu.Visibility = Visibility.Hidden;
            GameTypeTextBlock.Text = "Game mode: Pentamino";
            previousLevel = -1;
            await GameLoop();
        }
    }
}