using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Threading;
using WMPLib;

namespace Snake
{
    public partial class Form1 : Form
    {
        Random randomWorm = new Random();

        Graphics graphic;

        Snake snake = new Snake();
        Worm worm;

        bool up = false, down = false, left = false, right = false;

        bool isplaying = false;

        int score = 0;

        static WindowsMediaPlayer wplayer;

        public Form1()
        {
            InitializeComponent();

            Thread thread = new Thread(new ThreadStart(playMp3));
            thread.Start();

            worm = new Worm(randomWorm);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            graphic = e.Graphics;

            worm.DrawWorm(graphic);
            snake.DrawSnake(graphic);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //Nếu nhấn space không phải trong lúc chơi thì new game
            if (e.KeyData == Keys.Space && isplaying == false)
            {
                timer1.Enabled = true;

                isplaying = true;

                up = false; down = false; left = false; right = true;

                label1.Text = "";
                label3.Text = "";
            }

            if (e.KeyData == Keys.Up && down == false)
            {
                up = true;
                down = false;
                left = false;
                right = false;
            }

            if (e.KeyData == Keys.Down && up == false)
            {
                up = false;
                down = true;
                left = false;
                right = false;
            }

            if (e.KeyData == Keys.Left && right == false)
            {
                up = false;
                down = false;
                left = true;
                right = false;
            }

            if (e.KeyData == Keys.Right && left == false)
            {
                up = false;
                down = false;
                left = false;
                right = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabelScore.Text = score.ToString();

            if (up == true)
                snake.MoveUp();

            if (down == true)
                snake.MoveDown();

            if (left == true)
                snake.MoveLeft();

            if (right == true)
                snake.MoveRight();

            //Rắn ăn thức ăn
            if (snake.SnakeRects[0].IntersectsWith(worm.wormRect))
            {
                score += 10;
                snake.GrowSnake();
                worm.WormLocation(randomWorm);
            }
            
            //Kiểm tra xem rắn có cắn vào thân hay đâm vào tường không
            collision();
            this.Invalidate();
        }

        /// <summary>
        /// Kiểm tra rắn tự cắn vào thân hoặc đâm vào tường
        /// </summary>
        private void collision()
        {
            for (int i = 1; i < snake.SnakeRects.Length; i++)
                if (snake.SnakeRects[0].IntersectsWith(snake.SnakeRects[i]))
                {
                    restart();
                }

            if (snake.SnakeRects[0].X < 3 || snake.SnakeRects[0].X > 347)
            {
                restart();
            }

            if (snake.SnakeRects[0].Y < 3 || snake.SnakeRects[0].Y > 347)
            {
                restart();
            }
        }

        //Kết thúc game, chuẩn bị cho ván mới
        private void restart()
        {
            timer1.Enabled = false;
            isplaying = false;
            label1.Text = "Press Space to start game";
            label3.Text = "Arrow Key to move";
            MessageBox.Show("Snake dead!\nYour score: " + score.ToString());
            toolStripStatusLabelScore.Text = "0";
            score = 0;
            up = false; down = false; left = false; right = false;
            snake = new Snake();
        }

        private void playMp3()
        {
            wplayer = new WindowsMediaPlayer();

            string exePath = Assembly.GetExecutingAssembly().Location;
            string folder = Path.GetDirectoryName(exePath);
            FileInfo[] musicFile = new DirectoryInfo(folder).GetFiles("*.mp3");

            if (musicFile.Length != 0)
            {
                wplayer.URL = musicFile[0].FullName;
                wplayer.controls.play();
            }
        }
    }
}
