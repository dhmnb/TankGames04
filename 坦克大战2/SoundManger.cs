using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using 坦克大战2.Properties;

namespace 坦克大战2
{
    class SoundManger
    {
        private static SoundPlayer startPlayer = new SoundPlayer();
        private static SoundPlayer addPlayer = new SoundPlayer();
        private static SoundPlayer blastPlayer = new SoundPlayer();
        private static SoundPlayer firePlayer = new SoundPlayer();
        private static SoundPlayer hitPlayer = new SoundPlayer();
        public static void InitSound() 
        {
            addPlayer.Stream = Resources.add;
            startPlayer.Stream = Resources.start;

            blastPlayer.Stream = Resources.blast;
            firePlayer.Stream = Resources.fire;
            hitPlayer.Stream = Resources.hit;
        }
        public static void PlayStart() 
        {
            startPlayer.Play();
        }
        public static void PlayAdd()
        {
            addPlayer.Play();
        }
        public static void PlayBlast()
        {
            blastPlayer.Play();
        }
        public static void PlayFire()
        {
            firePlayer.Play();
        }
        public static void PlayHit()
        {
            hitPlayer.Play();
        }
    }
}
