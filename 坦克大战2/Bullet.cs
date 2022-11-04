using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 坦克大战2.Properties;

namespace 坦克大战2
{
    enum Tag 
    {
        MyTank,
        EnemyTank
    }
    //子弹
    class Bullet:MoveThing
    {
        public static int score = 0;
        public static String score1;
        public Tag Tag { get; set; }
        public bool IsDestroy { get; set; }
        public Bullet(int x, int y, int speed,Direction dir,Tag tag)
        {
            IsDestroy = false;
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            BitmapDown = Resources.BulletDown;
            BitmapUp = Resources.BulletUp;
            BitmapLeft = Resources.BulletLeft;
            BitmapRight = Resources.BulletRight;
            this.Dir = dir;

            this.X -= Width / 2;
            this.Y -= Height / 2;
            this.Tag = tag;
        }
        public override void DrawSelf()
        {
            base.DrawSelf();
        }
        public override void UpDate()
        {
            MoveCheck();
            Move();
            base.UpDate();
        }
        private void MoveCheck()
        {
            //检查查出边界
            if (Dir == Direction.Up)
            {
                if (Y +Height/2+3< 0)
                {
                    IsDestroy=true; return;
                }
            }
            else if (Dir == Direction.Down)
            {
                if (Y + Height / 2-3 > 450)
                {
                    IsDestroy = true; return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X + Width/2-3 < 0)
                {
                    IsDestroy = true; return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X + Width/2+3 > 450)
                {
                    IsDestroy = true; return;
                }
            }
            //检查有没有和元素发生碰撞
            Rectangle rect = getRectangle();
            rect.X = X + Width / 2 - 3;
            rect.Y = Y + Height / 2 - 3;
            rect.Height = 3;
            rect.Width = 3;
            int xExplosion = this.X + Width / 2;
            int yExplosion = this.Y + Height / 2;
            NotMoveThing wall = null;
            //跟墙发生碰撞
            if ((wall = GameObjectManger.IsCollededWall(rect)) != null)
            {
                IsDestroy=true;
                GameObjectManger.DestroyWall(wall);
                //爆炸效果
                GameObjectManger.CreateExplosion(xExplosion,yExplosion);
                SoundManger.PlayBlast();
                return;
            }
            //跟钢墙发生碰撞
            if (GameObjectManger.IsCollededSteel(rect) != null)
            {
                IsDestroy = true;
                GameObjectManger.CreateExplosion(xExplosion, yExplosion);
                return;
            }
            if (GameObjectManger.IsColliededBoss(rect))
            {
                SoundManger.PlayBlast();
                GameFramework.ChangeToGameOver(); return;
            }
            //跟坦克发生碰撞
            if (Tag == Tag.MyTank) 
            {
                EnemyTank tank = null;
                if ((tank = GameObjectManger.IsCollidedEnemyTank(rect)) != null)
                {
                    IsDestroy = true;
                    GameObjectManger.DestroyTank(tank);
                    score++;
                    score1 = score.ToString();

                    GameObjectManger.CreateExplosion(xExplosion, yExplosion);
                    SoundManger.PlayHit();
                    return;
                }
            }
            //子弹跟敌人坦克发生爆炸
            else if (Tag == Tag.EnemyTank)
            {
                MyTank mytank = null;
                if ((mytank = GameObjectManger.IsCollidedMyTank(rect)) != null) 
                {
                    IsDestroy = true;
                    GameObjectManger.CreateExplosion(xExplosion, yExplosion);
                    SoundManger.PlayBlast();
                    mytank.TakeDamage();
                    return;
                }
            }
            
        }
        private void Move()
        {

            switch (Dir)
            {
                case Direction.Up:
                    Y -= Speed;
                    break;
                case Direction.Down:
                    Y += Speed;
                    break;
                case Direction.Left:
                    X -= Speed;
                    break;
                case Direction.Right:
                    X += Speed;
                    break;
            }
        }
        public static void DrawScore(Graphics g,PaintEventArgs e)
        {          
            
            g.DrawString(score1, new Font("楷体", 100), new SolidBrush(Color.Black), new PointF(500, 120));
        }

        

    }
}
