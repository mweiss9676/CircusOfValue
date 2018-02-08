using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using Capstone.Classes.Items;

namespace Capstone.Classes
{
    public class VendingMachineFileReader
    {
        static void Main(string[] args)
        {
            List<VendingMachineItem> newInventory = new List<VendingMachineItem>();

            try
            {
                using (StreamReader sr = new StreamReader("InputInventory.txt"))
                {
                    while(!sr.EndOfStream)
                    {
                        string[] line = sr.ReadLine().Split('|');

                        for (int i = 0; i < line.Length; i++)
                        {
                            VendingMachineItem chips = new ChipItem(line[0]);
                           
                        }
                    }
                }

            }
            catch(Exception)
            {
                Console.WriteLine("nah bruj");
            }
        }
      
    }
}
