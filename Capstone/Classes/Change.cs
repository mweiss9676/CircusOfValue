﻿using System;
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
     

    }
}
