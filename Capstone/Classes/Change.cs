using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class Change
    {
        public int Nickels { get; private set; }

        public int Dimes { get; private set; }

        public int Quarters { get; private set; }

        public decimal TotalChange { get; private set; }

        public int NumberOfCoinsTotal { get; private set; }

        public void CalculateChange(decimal currentMoneyProvided, decimal totalCart)
        {
            decimal change = 0.0M;

            change = currentMoneyProvided - totalCart;

            TotalChange = change;

            while (change - .25M >= 0.0M)
            {
                change -= .25M;
                Quarters++;
                NumberOfCoinsTotal++;
            }
            while (change - .10M >= 0.0M)
            {
                change -= .10M;     
                Dimes++;
                NumberOfCoinsTotal++;
            }
            while (change - .05M >= 0.0M)
            {
                change -= .05M;
                Nickels++;
                NumberOfCoinsTotal++;
            }
        }

        public void ChangeSound()
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
