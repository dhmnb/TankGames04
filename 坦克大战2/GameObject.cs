using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 坦克大战2
{
    abstract class GameObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        //因为每个元素都需要绘制，所以将绘制元素的方法放在父类中并 声明为抽象
        public int Width { get; set; }
        public int Height { get; set; }
        protected abstract Image GetImage(); 
        
        public virtual void DrawSelf() 
        {
            Graphics g = GameFramework.g;
            g.DrawImage(GetImage(),X,Y);
        }
        public virtual void UpDate() 
        {
            DrawSelf();
        }
        //用于获取矩形的方法
        public Rectangle getRectangle()
        {
            Rectangle rectangle = new Rectangle(X, Y, Width, Height);
            return rectangle;
        }

    }
}
