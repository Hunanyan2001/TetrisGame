using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Tetris
{
    public class Tetromino
    {
        private Point poss;
        private Point[] possitionShape;
        private Brush Color;
        private bool rotate;
        public Tetromino()
        {
            poss = new Point(0, 0);
            Color = Brushes.Transparent;
            possitionShape = CreateTetrominoPossition();
        }

        public Brush getColor()
        {
            return Color;
        }

        public Point getPossitionInBoard()
        {
            return poss;
        }

        public Point[] getPossitionShape()
        {
            return possitionShape;
        }
        public void MoveLeft()
        {
            poss.Y -= 1;
        }

        public void MoveRight()
        {
            poss.Y += 1;
        }

        public void MoveDown()
        {
            poss.X += 1;
        }

        public void Rotate()
        {
            if (rotate)
            {
                for (int i = 0; i < possitionShape.Length; i++)
                {
                    double x = possitionShape[i].X;
                    possitionShape[i].X = possitionShape[i].Y * -1;
                    possitionShape[i].Y = x;
                }
            }
        }


        public Point[] CreateTetrominoPossition()
        {
            Random rnd = new Random();
            switch (rnd.Next()%7)
            {
                case 0://I
                    rotate = true;
                    Color = Brushes.CadetBlue;
                    return new Point[]
                    {
                        new Point(-1,0),
                        new Point(0,0),
                        new Point(1,0),
                        new Point(2,0)
                    };
                case 1://J
                    rotate = true;
                    Color = Brushes.Blue;
                    return new Point[]
                    {
                        new Point(0, -1),
                        new Point(-1, 0),
                        new Point(0, 0),
                        new Point(1, 0)
                    };
                case 2://L
                    rotate = true;
                    Color = Brushes.Green;
                    return new Point[]
                    {
                        new Point(0, 0),
                        new Point(-1, 0),
                        new Point(1, 0),
                        new Point(1, -1)
                    };
                case 3://O
                    rotate = true;
                    Color = Brushes.Green;
                    return new Point[]
                    {
                        new Point(0, 0),
                        new Point(0, 1),
                        new Point(1, 0),
                        new Point(1, 1)
                    };
                case 4://S
                    rotate = true;
                    Color = Brushes.Brown;
                    return new Point[]
                    {
                        new Point(0, 0),
                        new Point(-1, 0),
                        new Point(0, -1),
                        new Point(1, 0)
                    };
                case 5://T
                    rotate = true;
                    Color = Brushes.Black;
                    return new Point[]
                    {
                        new Point(0, 0),
                        new Point(-1, 0),
                        new Point(0, -1),
                        new Point(1, 0)
                    };
                case 6://Z
                    rotate = true;
                    Color = Brushes.Yellow;
                    return new Point[]
                    {
                        new Point(0, 0),
                        new Point(-1, 0),
                        new Point(0, 1),
                        new Point(1, 1)
                    };
                default:
                    return null;
            }
        }
    }
}
