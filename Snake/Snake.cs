using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Linq;

namespace Snake
{
    class Snake
    {
        private Rectangle[] snakeRects;

        public Rectangle[] SnakeRects
        {
            get { return snakeRects; }
            set { snakeRects = value; }
        }

        private int x, y, width, height;

        private SolidBrush brush;

        /// <summary>
        /// Khởi tạo rắn
        /// </summary>
        public Snake()
        {
            SnakeRects = new Rectangle[3];

            brush = new SolidBrush(Color.Blue);

            x = 25; y = 5; width = 10; height = 10;

            for (int i = 0; i < SnakeRects.Length; i++)
            { 
                SnakeRects[i] = new Rectangle(x,y,width,height);
                x -= 10;
            }
        }

        /// <summary>
        /// Vẽ rắn
        /// </summary>
        /// <param name="grp"></param>
        public void DrawSnake(Graphics grp)
        {
            foreach(Rectangle rect in SnakeRects)
                grp.FillEllipse(brush,rect);
        }

        /// <summary>
        /// Vẽ rắn khi di chuyển
        /// </summary>
        /// <param name="grp"></param>
        public void DrawSnakeRun()
        {
            for (int i = SnakeRects.Length - 1; i > 0; i--)
            {
                SnakeRects[i] = SnakeRects[i - 1];
            }
        }

        public void MoveUp()
        {
            DrawSnakeRun();
            SnakeRects[0].Y -= 10;
        }

        public void MoveDown()
        {
            DrawSnakeRun();
            SnakeRects[0].Y += 10;
        }

        public void MoveLeft()
        {
            DrawSnakeRun();
            SnakeRects[0].X -= 10;
        }

        public void MoveRight()
        {
            DrawSnakeRun();
            SnakeRects[0].X += 10;
        }

        public void GrowSnake()
        {
            List<Rectangle> rect = snakeRects.ToList();
            rect.Add(new Rectangle(SnakeRects[SnakeRects.Length - 1].X, SnakeRects[SnakeRects.Length - 1].Y, width, height));
            snakeRects = rect.ToArray();
        }
    }
}
