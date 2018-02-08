using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone;


namespace Capstone.Classes
{
    public class VendingMachineCMDLI
    {
        static VendingMachineLogic machine = new VendingMachineLogic();

        static void Main(string[] args)
        {
            

            while (true)
            {
                try
                {
                    

                   

                    

                   
                    break;

                }
                //Add more specific exception later
                catch (Exception)
                {
                    Console.WriteLine("nah bruj");
                }
            }
        }
        private static void PrintDisplayMenu(VendingMachineLogic machine)
        {
            Console.WriteLine("(1) Display Items");
            Console.WriteLine();
            Console.WriteLine("(2) Purchase Items");
            int mainMenuResult = int.Parse(Console.ReadLine());

            if(mainMenuResult == 1)
            {
                PrintDisplayItems(machine);
            }
            else if(mainMenuResult == 2)
            {
                PrintPurchaseMenu(mainMenuResult, machine);
            }

        }
        private static void PrintDisplayItems(VendingMachineLogic machine)
        {
            foreach (var kvp in machine.Inventory)
            {
                Console.WriteLine($"{kvp.Key} | {kvp.Value[0].NameOfItem} | {kvp.Value[2].PriceOfItem}");
            }
        }
    private static void PrintPurchaseMenu(int mainMenuResult, VendingMachineLogic machine)
        {
           
           
                 Console.WriteLine("(1) Feed Money");
                    int feedMoney = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.WriteLine("(2) Select Product");
                    int selectProduct = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.WriteLine("(3) Finish Transaction");
                    int finishTransaction = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.WriteLine($"Current Money Provided: {machine.CurrentMoneyProvided} ");
            
        }
    }
}
