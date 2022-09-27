using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tetris
{
    public class Board
    {
        private int Rows;
        private int Columns;
        private int Score;
        private int LinesFilled;
        private Tetromino tetrominoFigure;
        private Label[,] BlockControls;

        static private Brush NoBrush = Brushes.Transparent;
        static private Brush SilverBrush = Brushes.Black;
        public Board(Grid BaseGrid)
        {
            Rows = BaseGrid.RowDefinitions.Count;
            Columns = BaseGrid.ColumnDefinitions.Count;
            Score = 0;
            LinesFilled = 0;
            BlockControls = new Label[Rows, Columns];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {

                    BlockControls[i, j] = new Label();
                    BlockControls[i, j].Background = NoBrush;
                    BlockControls[i, j].BorderBrush = SilverBrush;
                    BlockControls[i, j].BorderThickness = new Thickness(1, 1, 1, 1);
                    Grid.SetColumn(BlockControls[i, j], j);
                    Grid.SetRow(BlockControls[i, j], i);
                    BaseGrid.Children.Add(BlockControls[i, j]);
                }
            }
            tetrominoFigure = new Tetromino();
            tetrominoFigureDraw();
        }

        public void tetrominoFigureDraw()
        {
            Point[] possitionShape = tetrominoFigure.getPossitionShape();
            Point possitionBoard = tetrominoFigure.getPossitionInBoard();
            Brush tetrominoColor = tetrominoFigure.getColor();
            foreach (Point figure in possitionShape)
            {
                var positionX = (int)(figure.X + possitionBoard.X) + 1;
                var positionY = (int)(figure.Y + possitionBoard.Y) + (Columns / 2);
                if (positionY < 0)
                {
                    positionY = 0;
                }
                if (positionY >= Columns)
                {
                    positionY = 10;
                }
                BlockControls[positionX, positionY].Background = tetrominoColor;
            }
        }

        public void tetrominoFigureDelete()
        {
            Point[] possitionShape = tetrominoFigure.getPossitionShape();
            Point possitionBoard = tetrominoFigure.getPossitionInBoard();
            Brush tetrominoColor = tetrominoFigure.getColor();
            foreach (Point figure in possitionShape)
            {
                BlockControls[(int)(figure.X + possitionBoard.X) + 1, (int)(figure.Y + possitionBoard.Y) + (Columns / 2)].Background = NoBrush;
            }
        }
        public void TetrominoFigureDown()
        {
            Point[] possitionShape = tetrominoFigure.getPossitionShape();
            Point possitionBoard = tetrominoFigure.getPossitionInBoard();
            Brush tetrominoColor = tetrominoFigure.getColor();
            bool moveTetromino = true;
            tetrominoFigureDelete();
            foreach (Point figure in possitionShape)
            {
                if (((int)(figure.X + possitionBoard.X) + 1) >= Rows - 1)
                {
                    moveTetromino = false;
                }

                else if (BlockControls[(int)(figure.X + possitionBoard.X) + 1 + 1,//ira x kordinant@ +1 ova iranic mi hat nerqev +1+1 na
                            (int)(figure.Y + possitionBoard.Y) + (Columns / 2)].Background != NoBrush)
                {
                    moveTetromino = false;
                }
            }
            if (moveTetromino == true)
            {
                tetrominoFigure.MoveDown();
                tetrominoFigureDraw();
            }
            else
            {
                tetrominoFigureDraw();
                CheckRows();
                tetrominoFigure = new Tetromino();
            }
        }

        public void TetrominoFigureLeft()
        {
            Point[] possitionShape = tetrominoFigure.getPossitionShape();
            Point possitionBoard = tetrominoFigure.getPossitionInBoard();
            bool moveTetromino = true;
            tetrominoFigureDelete();
            foreach (Point figure in possitionShape)
            {
                if (((int)(figure.Y + possitionBoard.Y) + ((Columns / 2))) <= 0)
                {
                    moveTetromino = false;
                }
                else if (BlockControls[(int)(figure.X + possitionBoard.X) + 1,
                (int)(figure.Y + possitionBoard.Y) + (Columns / 2) - 1].Background != NoBrush)// - 1 figuri y kordinantic dzax ete guynova dast@ uremn figur@ chsharjvi  
                {
                    moveTetromino = false;
                }
            }
            if (moveTetromino == true)
            {
                tetrominoFigure.MoveLeft();
                tetrominoFigureDraw();
            }
            else
            {
                tetrominoFigureDraw();
            }
        }

        public void TetrominoFigureRight()
        {
            Point[] possitionShape = tetrominoFigure.getPossitionShape();
            Point possitionBoard = tetrominoFigure.getPossitionInBoard();
            bool moveTetromino = true;
            tetrominoFigureDelete();
            foreach (Point figure in possitionShape)
            {
                if ((int)(figure.Y + possitionBoard.Y) + (Columns / 2) >= Columns - 1)
                {
                    moveTetromino = false;
                }
                else if (BlockControls[(int)(figure.X + possitionBoard.X) + 1,
                (int)(figure.Y + possitionBoard.Y) + (Columns / 2) + 1].Background != NoBrush)//+1 vorovhetev aj y kordinantic mi hat avel ete guynova figur@ kaynacni
                {
                    moveTetromino = false;
                }
            }
            if (moveTetromino == true)
            {
                tetrominoFigure.MoveRight();
                tetrominoFigureDraw();
            }
            else
            {
                tetrominoFigureDraw();
            }
        }

        public void TetrominoFigureRotate()
        {
            Point[] S = new Point[4];
            Point[] Shape = tetrominoFigure.getPossitionShape();
            var possition = tetrominoFigure.getPossitionInBoard();
            bool move = true;
            Shape.CopyTo(S, 0);
            tetrominoFigureDelete();
            for (int i = 0; i < S.Length; i++)
            {
                if (possition.Y + Columns/2 <= 0)
                {
                    possition.Y  = Columns / 2;
                    move = false;
                }
                if (possition.Y + Columns/2 >= Columns - 1)
                {
                    possition.Y = Columns - 1;
                    move = false;
                }
            }
            if (move)
            {
                tetrominoFigure.Rotate();
                tetrominoFigureDraw();
            }
            else
            {
                tetrominoFigureDraw();
            }
        }

        private void CheckRows()
        {
            bool full;
            for (int i = Rows - 1; i > 0; i--)
            {
                full = true;
                for (int j = 0; j < Columns; j++)
                {
                    if (BlockControls[i, j].Background == NoBrush)
                    {
                        full = false;
                    }
                }
                if (full)
                {
                    RemoveRow(i);
                    Score += 100;
                    LinesFilled += 1;
                }
            }
        }

        public bool GameOver()
        {
            Brush tetrominoColor = tetrominoFigure.getColor();
            bool over = false;
            for (int i = 0; i < 11; i++)
            {
                if (BlockControls[0, 5].Background == tetrominoColor && BlockControls[20, i].Background != NoBrush)
                {
                    over = true;
                }
            }
            return over;
        }

        private void RemoveRow(int row)
        {
            for (int i = row; i > 2; i--)
            {
                for (int j = 0; j < Columns; j++)
                {
                    BlockControls[i, j].Background = BlockControls[i - 1, j].Background;
                }
            }
        }
    }
}
