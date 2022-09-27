using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        Board myBoard;
        Direction direction;
        public double row = 0;
        public double columns = 0;
        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Tick += timerTick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            //timer.Start();
            myBoard = new Board(MainGrid);
        }

        public void GameStart()
        {
            myBoard.TetrominoFigureDown();
            timer.Start();
            PlaySound();
        }

        public void GamePause()
        {
            timer.Stop();
        }

        public void PlaySound()
        {
            var __mediaPlayer = new MediaPlayer();
            var executionDirectory = Environment.CurrentDirectory;
            var path = System.IO.Path.Combine(executionDirectory, "tetris-gameboy-02.mp3");
            __mediaPlayer.Open(new Uri(path));
            __mediaPlayer.Play();
        }

        private void timerTick(object sender, EventArgs e)
        {
            if (direction == Direction.Buttom)
            {
                myBoard.TetrominoFigureDown();
                if (myBoard.GameOver() == true)
                {
                    MessageBox.Show("GameOver");
                    this.Close();
                }
            }
            if (direction == Direction.Left)
            {
                myBoard.TetrominoFigureLeft();
                direction = Direction.Buttom;
            }
            if (direction == Direction.Right)
            {
                myBoard.TetrominoFigureRight();
                direction = Direction.Buttom;
            }
            if (direction == Direction.Top)
            {
                myBoard.TetrominoFigureRotate();
                direction = Direction.Buttom;
            }
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                direction = Direction.Left;
            }
            if (e.Key == Key.Right)
            {
                direction = Direction.Right;
            }
            if (e.Key == Key.Up)
            {
                direction = Direction.Top;
            }
            if (e.Key == Key.Down)
            {
                direction = Direction.Buttom;
            }
            if (e.Key == Key.P)
            {
                GamePause();
            }
            if (e.Key == Key.Enter)
            {
                GameStart();
            }
            if (e.Key == Key.Q)
            {
                PlaySound();
            }
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            GameStart();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            GamePause();
        }
    }
}
