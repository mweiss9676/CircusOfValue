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

                    VendingMachineLogic machine = new VendingMachineLogic();

                    foreach (var kvp in machine.Inventory)
                    {
                        Console.WriteLine($"{kvp.Key}| {kvp.Value[0].NameOfItem} {kvp.Value[2].PriceOfItem}");
                    }
                    Console.WriteLine();
                    Console.WriteLine("(2) Purchase");
                    int purchaseItems = int.Parse(Console.ReadLine());

                    Console.WriteLine("(1) Feed Money");
                    int feedMoney = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.WriteLine("(2) Select Product");
                    int selectProduct = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.WriteLine("(3) Finish Transaction");
                    int finishTransaction = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.WriteLine($"Current Money Provided: ***");
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
