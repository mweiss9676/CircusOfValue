using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Sounds
{
    public class AudioPlayer
    {
        public static void ChangeSound()
        {

            System.Media.SoundPlayer register = new System.Media.SoundPlayer(@"Sounds\cashregister.wav");
            System.Media.SoundPlayer change = new System.Media.SoundPlayer(@"Sounds\change_falling.wav");
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"Sounds\coinreturn.wav");

            //while (NumberOfCoinsTotal > 0)
            //{
            //    register.Play();
            //    System.Threading.Thread.Sleep(325);
            //    NumberOfCoinsTotal--;
            //}
            register.Play();
            System.Threading.Thread.Sleep(1500);
            player.Play();
            System.Threading.Thread.Sleep(325);
            player.Play();
            System.Threading.Thread.Sleep(325);
            player.Play();
            System.Threading.Thread.Sleep(325);

        }
    }
}
