using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Capstone.Classes
{
    public class VendingMachineCMDLI
    {
        static void Main(string[] args)
        {
            while(true)
            {
                try
                {
                    Console.WriteLine("(1) Display Items");
                    int displayItems = int.Parse(Console.ReadLine());

                    VendingMachine machine = new VendingMachine();
                    foreach (var kvp in machine.Inventory)
                    {
                        Console.WriteLine($"{kvp.Key} | {kvp.Value}");
                    }

                    Console.WriteLine("(2) Purchase");
                    int purchaseItems = int.Parse(Console.ReadLine());
                    break;

                }
                //Add more specific exception later
                catch (Exception)
                {
                    Console.WriteLine("nah bruj");
                }
            }
        }
    }
}
