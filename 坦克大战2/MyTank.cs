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
    //我方坦克
    class MyTank:MoveThing
    {
        public int HP { get; set; }
        public bool IsMoving { get; set; }
        private int originalX;
        private int originalY;
        public MyTank(int x, int y, int speed) 
        {
            IsMoving = false;
            this.X = x;
            this.Y = y;
            originalX = x;
            originalY = y;
            this.Speed = speed;
            BitmapDown = Resources.MyTankDown;
            BitmapUp = Resources.MyTankUp;
            BitmapLeft = Resources.MyTankLeft;
            BitmapRight = Resources.MyTankRight;
            this.Dir = Direction.Up;
            HP = 10;
            
        }
        
        public void KeyDown(KeyEventArgs args)
        {
            switch (args.KeyCode) 
            {
                case Keys.W:
                    Dir = Direction.Up;
                    IsMoving = true;
                    break;
                case Keys.S:
                    Dir = Direction.Down;
                    IsMoving = true;
                    break;
                case Keys.A:
                    Dir = Direction.Left;
                    IsMoving = true;
                    break;
                case Keys.D:
                    Dir = Direction.Right;
                    IsMoving = true;
                    break;
            }
        }
        public void KeyUp(KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.W:
                    Dir = Direction.Up;
                    IsMoving = false;
                    break;
                case Keys.S:
                    IsMoving = false;
                    break;
                case Keys.A:
                    IsMoving = false;
                    break;
                case Keys.D:
                    IsMoving = false;
                    break;
                case Keys.Space:
                    //发射子弹

                    Attack();
                    break;
            }

        }
        private void Attack() 
        {
            SoundManger.PlayFire();
            int x = this.X;
            int y = this.Y;
            switch (Dir) 
            {
                case Direction.Up:
                    x = x + Width / 2;
                    break;
                case Direction.Down:
                    x = x + Width / 2;
                    y += Height;
                    break;
                case Direction.Left:

                    y = y + Height / 2;
                    break;
                case Direction.Right:
                    x += Width ;
                    y = y + Height / 2;
                    break;
            }
            GameObjectManger.CreatBullet(x, y, Tag.MyTank, Dir);
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
                if (Y - Speed < 0)
                {
                    IsMoving = false; return;
                }
            }
            else if (Dir == Direction.Down)
            {
                if (Y + Speed + Height > 450)
                {
                    IsMoving = false; return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X - Speed < 0)
                {
                    IsMoving = false; return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X + Speed + Width > 450)
                {
                    IsMoving = false; return;
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
                IsMoving = false; return;
            }
            if (GameObjectManger.IsCollededSteel(rect) != null)
            {
                IsMoving = false; return;
            }
            if (GameObjectManger.IsColliededBoss(rect))
            {
                IsMoving = false; return;
            }

        }
        private void Move()
        {
            if (IsMoving == false) return;
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
        public void TakeDamage() 
        {
            HP--;
            if (HP <= 0) 
            {
                X = originalX;
                Y = originalY;
                HP = 10;

            }
        }
        public  void ChongzhiTank() 
        {
            X = originalX;
            Y = originalY;
        }
    }
}
