using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_Game_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int score = 0;
        Panel part;
        Panel apple = new Panel();
        List<Panel> snake = new List<Panel>();
        string direction = "right";

        private void timer1_Tick(object sender, EventArgs e)
        {
            int snakeX = snake[0].Location.X;
            int snakeY = snake[0].Location.Y;

            skorCheck();
            removeApple();
            snakeTail();
            crash();

            if (direction == "right" && direction != "left")
            {
                if (snakeX < 480)
                    snakeX += 20;
                else snakeX = 0;
            }
            if (direction == "left" && direction != "right")
            {
                if (snakeX > 0)
                    snakeX -= 20;
                else snakeX = 480;
            }
            if (direction == "up" && direction != "down")
            {
                if (snakeY > 0)
                    snakeY -= 20;
                else snakeY = 480;
            }
            if (direction == "down" && direction != "up")
            {
                if (snakeY < 480)
                    snakeY += 20;
                else snakeY = 0;
            }
            snake[0].Location = new Point(snakeX, snakeY);
        }

        void createApple()
        {
            Random appleLoc = new Random();
            int appleX, appleY;
            appleX = appleLoc.Next(300);
            appleY = appleLoc.Next(300);

            appleX -= appleX % 20;
            appleY -= appleY % 20;

            apple.Size = new Size(20, 20);
            apple.BackColor = Color.Red;
            apple.Location = new Point(appleX, appleY);
            panel1.Controls.Add(apple);
        }
        void skorCheck()
        {
            switch (score)
            {
                case 50: timer1.Interval = 90; break;
                case 100: timer1.Interval = 80; break;
                case 150: timer1.Interval = 70; break;
                case 200: timer1.Interval = 60; break;
                case 250:timer1.Interval = 50; break;
            }
        }
        void removeApple()
        {
            if (snake[0].Location == apple.Location)
            {
                score += 10;
                skorLbl.Text = score.ToString();
                panel1.Controls.Remove(apple);
                createApple();
                addPart();
            }
        }
        void addPart()
        {
            Panel newpart = new Panel();
            newpart.Size = new Size(20, 20);
            newpart.BackColor = Color.DarkGreen;
            snake.Add(newpart);
            panel1.Controls.Add(newpart);
        }
        void snakeTail()
        {
            for (int i = snake.Count - 1; i > 0; i--)
            {
                snake[i].Location = snake[i - 1].Location;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int snakeX = snake[0].Location.X;
            int snakeY = snake[0].Location.Y;

            if (e.KeyCode == Keys.Right)
                direction = "right";

            if (e.KeyCode == Keys.Left)
                direction = "left";

            if (e.KeyCode == Keys.Up)
                direction = "up";

            if (e.KeyCode == Keys.Down)
                direction = "down";

            snake[0].Location = new Point(snakeX, snakeY);
        }

        void crash()
        {
            for (int i = 2; i < snake.Count; i++)
            {
                int score = int.Parse(skorLbl.Text);
                if (snake[0].Location == snake[i].Location)
                {
                    timer1.Stop();
                    MessageBox.Show("Game Over! Score: " + score);
                }
            }
        }

        private void btnStart_Click_1(object sender, EventArgs e)
        {
            score = 0;
            timer1.Stop();
            timer1.Interval = 100;
            skorLbl.Text = score.ToString();
            createApple();

            foreach (Panel part in snake)
            {
                panel1.Controls.Remove(part);
            }
            snake.Clear();

            part = new Panel();
            part.Location = new Point(200, 200);
            part.Size = new Size(20, 20);
            part.BackColor = Color.DarkGreen;
            snake.Add(part);
            panel1.Controls.Add(snake[0]);

            timer1.Start();

        }
    }
}
