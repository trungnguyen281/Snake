using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{    
    class Worm
    {
        public Rectangle wormRect;

        private SolidBrush brush;

        private int x, y, width, height;

        /// <summary>
        /// Khởi tạo con sâu (thức ăn của rắn)
        /// </summary>
        /// <param name="randomWorm"></param>
        public Worm(Random randomWorm)
        {
            x = randomWorm.Next(0, 32) * 10;
            y = randomWorm.Next(0, 32) * 10;

            width = 10; height = 10;

            brush = new SolidBrush(Color.Red);

            wormRect = new Rectangle(x, y, width, height);
        }

        /// <summary>
        /// Random vị trí mới cho sâu
        /// </summary>
        /// <param name="randomWorm"></param>
        public void WormLocation(Random randomWorm)
        {
            x = randomWorm.Next(0, 32) * 10;
            y = randomWorm.Next(0, 32) * 10;
        }

        /// <summary>
        /// Vẽ con sâu
        /// </summary>
        /// <param name="grp"></param>
        public void DrawWorm(Graphics grp)
        {
            wormRect.X = x;
            wormRect.Y = y;

            grp.FillEllipse(brush, wormRect);
        }
    }
}
