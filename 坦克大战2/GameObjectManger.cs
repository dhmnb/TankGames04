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
    class GameObjectManger
    {
       
        //创建管理墙的集合
        public static List<NotMoveThing> wallList = new List<NotMoveThing>();//管理土墙的集合
        public static List<NotMoveThing> steelList = new List<NotMoveThing>();//管理钢的集合
        public static List<Bullet> bulletList = new List<Bullet>();//管理子弹的集合
        private static NotMoveThing boss;
        private static MyTank myTank;
        public static List<EnemyTank> tankList = new List<EnemyTank>();//管理敌人坦克的集合
        public static List<Explosion> expList = new List<Explosion>();//管理爆炸效果的集合
        private static int enemyBornSpeed = 30;//敌人生成速度
        private static int enemyBornCount = 30;
        private static Point[] points = new Point[3];
        private static int count=0;
        
        
        
        public static void Start() 
        {         
            points[0].X = 0; points[0].Y = 0;//一号复活位置
            points[1].X = 7*30; points[1].Y = 0;//二号复活位置
            points[2].X = 14*30; points[2].Y = 0;//三号复活位置
        }
        
        public static void Update()
        {
            //遍历土墙的集合
            foreach (NotMoveThing nm in wallList)
            {
                nm.UpDate();
            }
            //遍历钢墙的集合
            foreach (NotMoveThing nm in steelList)
            {
                nm.UpDate();
            }
            foreach (EnemyTank tank in tankList) 
            {
                tank.UpDate();
            }
            for (int i =0;i< bulletList.Count;i++)
            {
                bulletList[i].UpDate();
            }
            CheckAndDestroyBullet();
            boss.UpDate();
            myTank.UpDate();
            //控制坦克数量
            if (count <= 7)
            {
                EnemyBorn();

            }
            //更换地图
            Huantu1();
            
            
            foreach (Explosion exp in expList) 
            {
                exp.UpDate();
            }
            CheckAndDestroyExplosion();
            




        }
        //消除子弹
        private static void CheckAndDestroyBullet() 
        {
            List<Bullet> needToDestroy = new List<Bullet>();
            foreach (Bullet bullet in bulletList) 
            {
                if (bullet.IsDestroy == true) 
                {
                    needToDestroy.Add(bullet);
                }
            }
            foreach (Bullet bullet in needToDestroy) 
            {
                bulletList.Remove(bullet);
            }
        }
        //控制生成子弹的方法
        public static void CreatBullet(int x,int y,Tag tag,Direction dir) 
        {
            Bullet bullet = new Bullet(x,y,5,dir,tag);
            bulletList.Add(bullet);
        }       
        //控制敌人生成的方法
        public static void EnemyBorn() 
        {
            enemyBornCount++;
           if (enemyBornCount < enemyBornSpeed) return;//没有到达生成敌人的时间直接renturn
            //SoundManger.InitSoundadd();
            //SoundManger.PlayAdd();
            Random rd = new Random();
            int index = rd.Next(0, 3);
            Point position = points[index];
            int enemyType = rd.Next(1, 5);
            

            switch (enemyType) 
            {
                case 1:
                    CreatEnemyTank1(position.X,position.Y);
                    break;
                case 2:
                    CreatEnemyTank2(position.X, position.Y);
                    break;
                case 3:
                    CreatEnemyTank3(position.X, position.Y);
                    break;
                case 4:
                    CreatEnemyTank4(position.X, position.Y);
                    break;
            }
            enemyBornCount = 0;
            count++;
            return;
        }
        private static void CreatEnemyTank1(int x,int y) 
        {
            EnemyTank tank = new EnemyTank(x,y,2,Resources.GrayDown, Resources.GrayUp, Resources.GrayRight, Resources.GrayLeft);
            tankList.Add(tank);
        }
        private static void CreatEnemyTank2(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 2, Resources.GreenDown, Resources.GreenUp, Resources.GreenRight, Resources.GreenLeft);
            tankList.Add(tank);
        }
        private static void CreatEnemyTank3(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 4, Resources.QuickDown, Resources.QuickUp, Resources.QuickRight, Resources.QuickLeft);
            tankList.Add(tank);
        }
        private static void CreatEnemyTank4(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 1, Resources.SlowDown, Resources.SlowUp, Resources.SlowRight, Resources.SlowLeft);
            tankList.Add(tank);
        }

        //绘制地图
        //public static void DrawMap() 
        //{ 
        //    //遍历土墙的集合
        //    foreach (NotMoveThing nm in wallList)
        //    {
        //         nm.DrawSelf();
        //    }
        //    //遍历钢墙的集合
        //    foreach (NotMoveThing nm in steelList)
        //    {
        //        nm.DrawSelf();
        //    }

        //    boss.DrawSelf();
        //}
        ////绘制坦克
        //public static void DrawMyTank() 
        //{
        //    myTank.DrawSelf();
        //}
        //创建我方坦克的方法
        public static void CreateMyTank() 
        {
            int x = 5 * 30;
            int y = 14 * 30;
            myTank = new MyTank(x, y, 5);
        }
        //擦黄健爆炸的效果
        public static void CreateExplosion(int x,int y) 
        {
            Explosion exp = new Explosion(x,y);
            expList.Add(exp);
        }
        //创建地图的的方法
        public static void CreateMap() 
        {
            //创建墙
            CreateWall(1, 1, 5,Resources.wall,wallList);
            CreateWall(3, 1, 5, Resources.wall, wallList);
            CreateWall(5, 1, 5, Resources.wall, wallList);
            CreateWall(7, 1, 5, Resources.wall, wallList);
            CreateWall(9, 1, 5, Resources.wall, wallList);
            CreateWall(11, 1, 5, Resources.wall, wallList);
            CreateWall(13, 1, 5, Resources.wall, wallList);
            CreateWall1(5, 8, 5, Resources.steel, steelList);
            CreateWall(1, 10, 2, Resources.wall, wallList);
            CreateWall(3, 10, 2, Resources.wall, wallList);
            CreateWall(5, 10, 2, Resources.wall, wallList);
            CreateWall(7, 10, 2, Resources.wall, wallList);
            CreateWall(9, 10, 2, Resources.wall, wallList);
            CreateWall(11, 10, 2, Resources.wall, wallList);
            CreateWall(13, 10, 2, Resources.wall, wallList);
            CreateWall(6, 13, 2, Resources.wall, wallList);
            CreateWall(7, 13, 1, Resources.wall, wallList);
            CreateWall(8, 13, 2, Resources.wall, wallList);
            //创建Boss
            CreateBoss(7, 14, 1, Resources.Boss);
            
        }
        public static void Click(EventArgs args) 
        {
            wallList.Clear();
            steelList.Clear();
            tankList.Clear();
            count = -5;
            CreateMap1();
        }
        public static void Huantu1() 
        {
            if (tankList.Count == 0) 
            {
                wallList.Clear();
                steelList.Clear();
                count = -5;
                CreateMap1();
                myTank.ChongzhiTank();
                
                

            }
        }
        
        //创建地图的的方法
        public static void CreateMap1()
        {
            //创建墙
            CreateWall(2, 1, 5, Resources.steel, steelList);

            //CreateWall(3, 1, 5, Resources.wall, wallList);
            //CreateWall(5, 1, 5, Resources.wall, wallList);
            //CreateWall(7, 1, 5, Resources.wall, wallList);
            //CreateWall(9, 1, 5, Resources.wall, wallList);
            //CreateWall(11, 1, 5, Resources.wall, wallList);
            CreateWall(13, 1, 3, Resources.wall, wallList);
            CreateWall1(5, 2, 2, Resources.steel, steelList);
            CreateWall1(9, 2, 2, Resources.steel, steelList);
            //CreateWall(1, 10, 2, Resources.wall, wallList);
            //CreateWall(3, 10, 2, Resources.wall, wallList);
            CreateWall(5, 10, 3, Resources.wall, wallList);
            CreateWall1(7, 8, 2, Resources.wall, wallList);
            CreateWall(9, 10, 2, Resources.wall, wallList);
            CreateWall1(2, 10, 2, Resources.wall, wallList);
            //CreateWall(13, 10, 2, Resources.wall, wallList);
            CreateWall(6, 13, 2, Resources.wall, wallList);
            CreateWall(7, 13, 1, Resources.wall, wallList);
            CreateWall(8, 13, 2, Resources.wall, wallList);
            //创建Boss
            CreateBoss(7, 14, 1, Resources.Boss);

        }
        
        //创建一个创建墙的方法 x代表横坐标的(四块墙的) y代表纵坐标的(四块墙的)
        //count代表墙(四块)的数量wallList集合用于保存所有qaing元素
        //c创建竖墙

        private static void CreateWall(int x,int y,int count,Image img, List<NotMoveThing> wallList) 
        {              
            int xPosition = x * 30;
            int yPosition = y * 30;
            //遍历每一列，相当遍历两次
            for (int i = yPosition;i<yPosition+count*30;i+=15) 
            {
                //i xPosition  i xPosition+15
                NotMoveThing wall1 = new NotMoveThing(xPosition,i,img); //创建左边的墙
                NotMoveThing wall2 = new NotMoveThing(xPosition+15, i, img); //创建右边的墙
                wallList.Add(wall1);
                wallList.Add(wall2);

            }
          

        }
        //创建横墙
        private static void CreateWall1(int x, int y, int count,Image img, List<NotMoveThing> wallList)
        {
            int xPosition = x * 30;
            int yPosition = y * 30;
            //遍历每一列，相当遍历两次
            for (int i = xPosition; i < xPosition + count * 30; i += 15)
            {
                //i xPosition  i xPosition+15
                NotMoveThing wall1 = new NotMoveThing(i, yPosition, img); //创建左边的墙
                NotMoveThing wall2 = new NotMoveThing(i, yPosition+15, img); //创建右边的墙
                wallList.Add(wall1);
                wallList.Add(wall2);

            }


        }
        //创建boss的方法
        private static void CreateBoss(int x, int y, int count, Image img)
        {
            int xPosition = x * 30;
            int yPosition = y * 30;
           
            boss = new NotMoveThing(xPosition, yPosition, img); //创建boss

        }
        public static void KeyDown(KeyEventArgs args) 
        {
            myTank.KeyDown(args);
        }
        public static void KeyUp(KeyEventArgs args)
        {
            myTank.KeyUp(args);
        }
        public static NotMoveThing IsCollededWall(Rectangle rt) 
        {
            foreach (NotMoveThing wall in wallList) 
            {
                if (wall.getRectangle().IntersectsWith(rt)) 
                {
                    return wall;
                }
            }
            return null;
        }
        //创建跟己方坦克发生时的碰撞检测的方法
        //public static MoveThing IsCollededTank(Rectangle rt)
        //{
        //    for (int i = 0; i < tankList.Count - 1; i++)
        //    {
        //        for (int j = 1; j < tankList.Count - 1 - i; j++)
        //            if (tankList[i].getRectangle().IntersectsWith(tankList[j].getRectangle()))
        //            {
        //                return tankList[i];
        //            }

        //    }
        //    return null;
        //}
        public static NotMoveThing IsCollededSteel(Rectangle rt)
        {
            foreach (NotMoveThing wall in steelList)
            {
                if (wall.getRectangle().IntersectsWith(rt))
                {
                    return wall;
                }
            }
            return null;
        }
        public static EnemyTank IsCollidedEnemyTank(Rectangle rt) 
        {
            foreach (EnemyTank tank in tankList) 
            {
                if (tank.getRectangle().IntersectsWith(rt)) 
                {
                    return tank;
                }
            }
            return null;
        }
        public static bool IsColliededBoss(Rectangle rt) 
        {
            return boss.getRectangle().IntersectsWith(rt);
            
        }
        public static MyTank IsCollidedMyTank(Rectangle rt) 
        {
            if (myTank.getRectangle().IntersectsWith(rt)) return myTank;
            else return null;
        }
        public static void DestroyWall(NotMoveThing wall) 
        {
            wallList.Remove(wall);
        }
        public static int Score = 0;//记录分数的
        public static Action<int> ScoreUpdate;
        public static void DestroyTank(EnemyTank tank)
        {
            tankList.Remove(tank);
            Score++;
            ScoreUpdate?.Invoke(Score);
            
        }
        
        //检查是否需要销毁的的方法
        private static void CheckAndDestroyExplosion() 
        {
            List<Explosion> needToDestroy = new List<Explosion>();
            foreach (Explosion exp in expList)
            {
                if (exp.IsNeedDestroy == true)
                {
                    needToDestroy.Add(exp);
                }
            }
            foreach (Explosion exp in needToDestroy)
            {
                expList.Remove(exp);
            }
        }
        
    }
}
