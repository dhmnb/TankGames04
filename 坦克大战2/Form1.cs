using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 坦克大战2
{
    public partial class Form1 : Form
    {
        private Thread t;
        private static Bitmap tempBmp;
        
        private static Graphics windowG;//窗体画布
        
        
       

        public Form1()
        {
            
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            windowG = this.CreateGraphics();
            //创建线程
            t = new Thread(new ThreadStart(GameMainThread));
            



            //解决闪烁问题
            tempBmp = new Bitmap(450,450);
            Graphics bmpG =  Graphics.FromImage(tempBmp);
            GameFramework.g = bmpG;
            GameObjectManger.ScoreUpdate += (e) =>
            {
                this.Invoke(new Action(() =>
                {
                    Score_label.Text = "当前分数为:" + e;
                }));
            };


        }
        
        public static void GameMainThread()
        {

            GameFramework.Start();//游戏开始
            int sleeptime = 1000 / 60;
            while (true)
            {
                //gameframework.g.clear(color.black);
                DrawBackGround();
                GameFramework.Update();
                windowG.DrawImage(tempBmp, 0, 0);              
                Thread.Sleep(sleeptime);
                
            }
        }
        

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
           t.Abort();
           
        }

        //创建绘制背景的方法
        private static void DrawBackGround() 
        {
            Rectangle rect = new Rectangle(0, 0, 450, 450);
            SolidBrush b1 = new SolidBrush(Color.Black);
            GameFramework.g.FillRectangle(b1, rect);
        }
        
            
            
        

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            GameObjectManger.KeyDown(e);
            
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            GameObjectManger.KeyUp(e);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Enabled) 
            {
                //this.Focus();
                button1.Enabled = false;
                t.Start();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }
    }
}
