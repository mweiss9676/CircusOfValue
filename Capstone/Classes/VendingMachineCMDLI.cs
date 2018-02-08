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
                    PrintDisplayMenu();
                }
                //Add more specific exception later
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private static void PrintDisplayMenu()
        {
            Console.WriteLine("(1) Display Items");
            Console.WriteLine();
            Console.WriteLine("(2) Purchase Items");
            int mainMenuResult = int.Parse(Console.ReadLine());

            if(mainMenuResult == 1)
            {
                Console.Clear();
                PrintDisplayItems();
            }
            else if(mainMenuResult == 2)
            {
                Console.Clear();
                PrintPurchaseMenu();
            }
        }

        private static void PrintDisplayItems()
        {
            foreach (var kvp in machine.Inventory)
            {
                Console.WriteLine($"{kvp.Key} | {kvp.Value[0].NameOfItem} | {kvp.Value[2].PriceOfItem}");
            }
        }

        private static void PrintPurchaseMenu()
        {
            Console.WriteLine("(1) Feed Money");
            Console.WriteLine();
            Console.WriteLine("(2) Select Product");
            Console.WriteLine();
            Console.WriteLine("(3) Finish Transaction");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Current Money Provided: {machine.CurrentMoneyProvided} ");

            int subMenuResult = int.Parse(Console.ReadLine());

            if (subMenuResult == 1)
            {
                Console.Clear();
                PrintFeedMoneyDisplay();
            }
            else if (subMenuResult == 2)
            {
                Console.Clear();
                DisplayProductSelection();
            }
            else if (subMenuResult == 3)
            {
                PrintFinishTransactionMenu();
            }

        }

        private static void PrintFinishTransactionMenu()
        {
            throw new NotImplementedException();
        }

        private static void DisplayProductSelection()
        {

            foreach (var kvp in machine.Inventory)
            {
                Console.WriteLine($"{kvp.Key} | {kvp.Value[0].NameOfItem} | {kvp.Value[2].PriceOfItem}");
            }
            Console.WriteLine("Please Enter Your Selection (A1):");
            string ItemSelection = Console.ReadLine();

            if (machine.Inventory.Keys.Contains(ItemSelection))
            {
                machine.AddItemToCart(machine.Inventory[ItemSelection][0]);
            }
            
        }

        private static void PrintFeedMoneyDisplay()
        {
            while (true)
            {
                Console.WriteLine("Please Insert Money:");
                Console.WriteLine("$1");
                Console.WriteLine("$2");
                Console.WriteLine("$5");
                Console.WriteLine("$10");
                Console.WriteLine("$20");
                Console.WriteLine("(D)one inserting money.");
                Console.WriteLine();
                Console.WriteLine($"Money Inserted: ${machine.CurrentMoneyProvided}");
                string answer = Console.ReadLine();

                //
                //
                //This isn't working because answer is being parsed to a decimal no matter what
                //If the user enters "D" the parse fails.
                //
                //
                decimal moneyInserted = decimal.Parse(answer);

                if (moneyInserted == 1 || moneyInserted == 2 || moneyInserted == 5 ||
                    moneyInserted == 10 || moneyInserted == 20)
                {
                    Console.Clear();
                    machine.CurrentMoneyProvided += moneyInserted;
                }
                else if (answer.ToUpper() == "D" || answer.ToUpper() == "Done")
                {
                    Console.Clear();
                    PrintPurchaseMenu();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"{moneyInserted} is not a valid denomination. Please select one of the values above.");
                }
            }
        }
    }
}
