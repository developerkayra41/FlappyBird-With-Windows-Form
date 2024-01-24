using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace FlappyBird
{
    public partial class Form1 : Form
    {

        #region DEĞİŞKENLER
        int gravity = 10;
        int pipeSpeed = 10;
        int score = 0;
        int firstScore = 0;
        int lastScore = 0;
        int randomY;
        short distance;
        sbyte speedRate;

        Random rnd = new Random();
        #endregion

        #region YAPICI VE LOAD METODU
        public Form1()
        {
            InitializeComponent();

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region TİMER
        private void timer1_Tick(object sender, EventArgs e)
        {
            bird.Top += gravity;
            if (bird.Top <= 2) bird.Top = 3;

            pipeTop.Left -= pipeSpeed;
            pipeBot.Left -= pipeSpeed;

            if (pipeBot.Left < -85)
            {
                pipeTop.Left = 974;
                pipeBot.Left = 1000;
                randomY = rnd.Next(262, 381);
                pipeBot.Top = randomY;
                pipeTop.Top = pipeBot.Top - distance;
                score++; label1.Text = string.Format($"Score: {score}");
            }

            if (score % speedRate == 0 || score != 0) firstScore = score;

            if (score >= lastScore + speedRate && score < firstScore + speedRate)
            {
                lastScore += speedRate;
                pipeSpeed += 2;
            }

            if (bird.Bounds.IntersectsWith(pipeBot.Bounds) || bird.Bounds.IntersectsWith(pipeTop.Bounds)
                || bird.Bounds.IntersectsWith(zemin.Bounds))
            {
                GameOver();
                MessageBox.Show($"Oyun Bitti ! Skorunuz {score}", "Kaybettin !");
            }
        }
        #endregion

        #region OYUN BİTTİ METODU
        void GameOver()
        {
            timer1.Stop();
        }
        #endregion

        #region TUŞ KONTROLU 
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) gravity = -10;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) gravity = 10;
        }
        #endregion

        #region OYUN ZORLUĞU SEÇİMİ
        private void button1_Click(object sender, EventArgs e)
        {
            distance = 471; speedRate = 5; panel2.Dispose(); timer1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            distance = 430; speedRate = 3; panel2.Dispose(); timer1.Enabled = true;
        }
        #endregion
    }
}
