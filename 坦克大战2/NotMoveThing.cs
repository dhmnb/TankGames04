using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 坦克大战2.Properties;

namespace 坦克大战2
{
    /*
     * 不可以移动的元素
     */
    class NotMoveThing:GameObject
    {
        private Image img;
        public Image Img 
        { 
            get { return img; }
            set 
            { 
                img = value;
                Width = img.Width;
                Height = img.Height;

            
            }
        } 
           
        //重写父类方法
        protected override Image GetImage()
        {
            return Img;
        }
        //构造器
        public NotMoveThing(int x,int y,Image img) 
        {
            this.X = x;
            this.Y = y;
            this.Img = img;
        }
    }
}
