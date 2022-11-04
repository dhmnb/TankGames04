using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 坦克大战2
{
    //敌人坦克
    class EnemyTank:MoveThing
    {
        public int ChangeDirSpeed{ get; set; }
        private int changeDirCount = 0;
       public int AttackSpeed { get; set; }//控制敌人坦克发射子弹的速度
        private int attackCount = 0;
        
        private Random r = new Random();
        public EnemyTank(int x, int y, int speed,Bitmap bmpDown, Bitmap bmpUp, Bitmap bmpRight, Bitmap bmpLeft)
        {
            
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            BitmapDown = bmpDown;
            BitmapUp = bmpUp;
            BitmapLeft = bmpLeft;
            BitmapRight = bmpRight;
            this.Dir = Direction.Down;
            AttackSpeed = 60;
            ChangeDirSpeed= 60;
            
        }
        public override void UpDate()
        {
            MoveCheck();
            Move();
            AttackCheck();
            AutoChangeDirection();
            base.UpDate();
        }
        private void AutoChangeDirection() 
        {
            changeDirCount++;
            if (changeDirCount < ChangeDirSpeed) return;
            ChangeDirection();
            changeDirCount = 0;
        }
        private void MoveCheck()
        {
            //检查查出边界
            if (Dir == Direction.Up)
            {
                if (Y - Speed < 0)
                {
                    ChangeDirection(); return;
                }
            }
            else if (Dir == Direction.Down)
            {
                if (Y + Speed + Height > 450)
                {
                    ChangeDirection(); return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X - Speed < 0)
                {
                    ChangeDirection(); return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X + Speed + Width > 450)
                {
                    ChangeDirection(); return;
                }
            }


            //检查有没有和元素发生碰撞
            Rectangle rect = getRectangle();
            switch (Dir)
            {
                case Direction.Up:
                    rect.Y -= Speed;
                    break;
                case Direction.Down:
                    rect.Y += Speed;
                    break;
                case Direction.Left:
                    rect.X -= Speed;
                    break;
                case Direction.Right:
                    rect.X += Speed;
                    break;
            }
            if (GameObjectManger.IsCollededWall(rect) != null)
            {
                ChangeDirection(); return;
            }
            if (GameObjectManger.IsCollededSteel(rect) != null)
            {
                ChangeDirection(); return;
            }
            if (GameObjectManger.IsColliededBoss(rect))
            {
                ChangeDirection(); return;
            }
            //Rectangle rect1 = getRectangle();
            ////检查有没有跟坦克发生碰撞
            //MoveThing tank = null;
            //switch (Dir)
            //{
            //    case Direction.Up:
            //        if ((tank = GameObjectManger.IsCollededTank(rect1)) != null)
            //        {
            //            ChangeDirection();
            //        }
            //        break;
            //    case Direction.Down:
            //        if ((tank = GameObjectManger.IsCollededTank(rect1)) != null)
            //        {
            //            ChangeDirection();
            //        }
            //        break;
            //    case Direction.Left:
            //        if ((tank = GameObjectManger.IsCollededTank(rect1)) != null)
            //        {
            //            ChangeDirection();
            //        }
            //        break;
            //    case Direction.Right:
            //        if ((tank = GameObjectManger.IsCollededTank(rect1)) != null)
            //        {
            //            ChangeDirection();
            //        }
            //        break;
            //}
            
            


        }
        private void ChangeDirection() 
        {
            while (true) 
            {
                Direction dir = (Direction)r.Next(0, 4);
                if (dir == Dir) 
                {
                    continue;
                }
                {
                    Dir = dir;break; 
                }
            }
            MoveCheck();

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
        private void AttackCheck()
        {
            attackCount++;
            if (attackCount < AttackSpeed) return;
            Attack();
            attackCount = 0;
        }
        private void Attack()
        {

            int x = this.X;
            int y = this.Y;
            switch (Dir)
            {
                case Direction.Up:
                    x = x + Width / 2;
                    break;
                case Direction.Down:
                    x = x + Width / 2;
                    y += Height+5;
                    break;
                case Direction.Left:

                    y = y + Height / 2+5;
                    break;
                case Direction.Right:
                    x += Width + 5;
                    y = y + Height / 2;
                    break;
            }
            GameObjectManger.CreatBullet(x, y, Tag.EnemyTank, Dir);

        }
    }
}
