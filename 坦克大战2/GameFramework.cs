using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 坦克大战2
{
    enum GameState
    {
        Running, 
        GameOver
    }
    class GameFramework
    { private static Object _lock = new object();   
        public static Graphics g;
        private static GameState gameState = GameState.Running;
        //游戏开始时调用的方法
        public static void Start() 
        {          
            SoundManger.InitSound();
            GameObjectManger.Start();
           
            GameObjectManger.CreateMap();
            GameObjectManger.CreateMyTank();
            SoundManger.PlayStart(); 
        }
        
        //此方法用于绘制游戏元素    每次绘制都需要被线程调用Update方法
        public static void Update() 
        {
            //GameObjectManger.DrawMap();
            //GameObjectManger.DrawMyTank();
            
            if (gameState == GameState.Running)
            {
                GameObjectManger.Update();
            }
            else if (gameState==GameState.GameOver) 
            {
                GameOverUpdate();
            }

        }
        private static void GameOverUpdate() 
        {
            int x = 450 / 2 - Properties.Resources.GameOver.Width / 2;
            int y = 450 / 2 - Properties.Resources.GameOver.Height / 2;
            g.DrawImage(Properties.Resources.GameOver,x,y);
        }
        public static void ChangeToGameOver() 
        {
            gameState = GameState.GameOver;
        }
     
    }
}
