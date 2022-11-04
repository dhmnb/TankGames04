﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 坦克大战2
{/*
  * 可以移动的元素  有   我方坦克  敌方坦克  子弹  三个子类
  */
    enum Direction
    {
        Up =0,
        Down=1,
        Left=2,
        Right=3
    }
    class MoveThing:GameObject
    {
        
        private Object _lock = new object();
        public Bitmap BitmapUp { get; set; }
        public Bitmap BitmapDown { get; set; }
        public Bitmap BitmapLeft { get; set; }
        public Bitmap BitmapRight { get; set; }

        
        public int Speed { get; set; }
        private Direction dir;
        public Direction Dir 
        {

            get {return dir; } 
            set 
            {
                dir = value;
                Bitmap bmp = null; 
                switch (dir) 
                {
                    case Direction.Up:
                        bmp = BitmapUp;
                        break;
                    case Direction.Down:
                        bmp = BitmapDown;
                        break;
                    case Direction.Right:
                        bmp = BitmapRight;
                        break;
                    case Direction.Left:
                        bmp = BitmapLeft;
                        break;
                }
                lock (_lock) 
                {
                    Width = bmp.Width;
                    Height = bmp.Height;
                }
                

            }
        }

        protected override Image GetImage()
        {
            Bitmap bitmap = null;
            switch (Dir) 
            {   
                case Direction.Up:
                    bitmap = BitmapUp;
                    break;
                case Direction.Down:
                    bitmap = BitmapDown;
                    break;
                case Direction.Left:
                    bitmap =  BitmapLeft;
                    break;
                case Direction.Right:
                    bitmap = BitmapRight;
                    break;
            }
            bitmap.MakeTransparent(Color.Black);
            return bitmap;
        }
        public override void DrawSelf()
        {
            lock (_lock)
            {
                base.DrawSelf();
            }

        }
        
    }
}
