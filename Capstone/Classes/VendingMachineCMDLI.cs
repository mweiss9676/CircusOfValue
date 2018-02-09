using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            string mainMenuStringResult = Console.ReadLine();
            int mainMenuIntResult;
            int.TryParse(mainMenuStringResult, out mainMenuIntResult);

            if(mainMenuIntResult == 1)
            {
                Console.Clear();
                PrintDisplayItems();
            }
            else if(mainMenuIntResult == 2)
            {
                Console.Clear();
                PrintPurchaseMenu();
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"{mainMenuStringResult} is not a valid option, please select from the available choices.");
                Console.WriteLine();
            }
        }

        private static void PrintDisplayItems()
        {
            foreach (var kvp in machine.Inventory)
            {
                Console.WriteLine($"{kvp.Key} | {kvp.Value[0].NameOfItem} | {kvp.Value[2].PriceOfItem}");
            }
            Console.WriteLine();
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
            Console.WriteLine($"Current Money Provided: ${machine.CurrentMoneyProvided} ");
            string doneOption = Console.ReadLine();
            int subMenuResult;
            int.TryParse(doneOption, out subMenuResult);

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
                Console.Clear();
                PrintFinishTransactionMenu();
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"You Entered {doneOption}. This Is Not A Valid Option.");
                Console.WriteLine();
                PrintPurchaseMenu();
            }
        }

        private static void PrintFinishTransactionMenu()
        {
            Console.WriteLine($"Your Total Cart is: {machine.TotalCart}");
            Console.WriteLine();
            Console.WriteLine($"Your Current Money Inserted is: {machine.CurrentMoneyProvided}");
            Console.WriteLine();
            Console.WriteLine("Are You Ready To Complete The Transaction? (Y)es To Process the Transaction / (N)o To Be Returned To The Product Selection Screen:");
            string completeTransaction = Console.ReadLine();
            
            if (completeTransaction.ToUpper() == "Y" || completeTransaction.ToUpper() == "YES")
            {
                if (machine.CurrentMoneyProvided - machine.TotalCart >= 0)
                {
                    Console.Clear();
                    PrintCompleteTransaction();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Insufficient Funds, Please Insert More Money");
                    Console.WriteLine();
                    PrintPurchaseMenu();
                }
            }
            else if (completeTransaction.ToUpper() == "N" || completeTransaction.ToUpper() == "NO")
            {
                Console.Clear();
                PrintPurchaseMenu();
            }
        }

        private static void PrintCompleteTransaction()
        {

            Change change = machine.GetChange();
            Console.WriteLine($"Total Change: ${change.TotalChange}");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine();
            Console.WriteLine($"Quarter(s): {change.Quarters}");
            Console.WriteLine($"Dime(s): {change.Dimes}");
            Console.WriteLine($"Nickel(s): {change.Nickels}");

            

            Console.WriteLine();
            foreach (var item in machine.ShoppingCart)
            {
                Console.WriteLine($"You are eating {item.NameOfItem} {item.ItemYumYum()}");
            }

            while (machine.ShoppingCart.Count > 0)
            {
                machine.PrintLog(machine.ShoppingCart[0].NameOfItem + " " + machine.ShoppingCart[0].SlotID + " $" 
                    + machine.CurrentMoneyProvided + "     $" + (machine.CurrentMoneyProvided - machine.ShoppingCart[0].PriceOfItem).ToString());

                machine.RemoveItemsFromCart(machine.ShoppingCart[0]);
            }
            

            machine.CalculateTotalShoppingCart(machine.ShoppingCart);
            machine.ResetCurrentMoneyProvided();
            machine.PrintLog("GIVE CHANGE: $" + change.TotalChange.ToString() + "     $" + machine.CurrentMoneyProvided.ToString());
            Console.WriteLine();
            PrintDisplayMenu();
        }

        private static void DisplayProductSelection()
        {
            while (true)
            {

                foreach (var kvp in machine.Inventory)
                {
                    if (machine.GetCurrentInventory(kvp.Value) > 0)
                    {
                        Console.WriteLine($"{kvp.Key.PadRight(2)} | {kvp.Value[0].NameOfItem.PadRight(20)} | ${kvp.Value[0].PriceOfItem}");
                    }
                    else
                    {
                        Console.WriteLine("SOLD OUT".PadLeft(13));
                    }
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("(D)one");
                Console.WriteLine();
                Console.WriteLine("Please Enter Your Selection (A1):");
                Console.WriteLine();

                if (machine.ShoppingCart.Count > 0)
                {
                    Console.Write($"Your current cart: {machine.ShoppingCart[0].NameOfItem.PadRight(20)} | ${machine.ShoppingCart[0].PriceOfItem}");
                    Console.WriteLine();
                    for (int i = 1; i < machine.ShoppingCart.Count; i++)
                    {
                        Console.WriteLine($"                   {machine.ShoppingCart[i].NameOfItem.PadRight(20)} | ${machine.ShoppingCart[i].PriceOfItem}");
                    }
                    Console.WriteLine();
                    Console.WriteLine($"Remove selection? (ex. Remove Potato Crisps)");
                    Console.WriteLine();
                    Console.WriteLine("---------------------------------------------");
                    machine.CalculateTotalShoppingCart(machine.ShoppingCart);
                    Console.WriteLine($"Total:".PadRight(40) + "| " + "$" + $"{machine.TotalCart}");
                    Console.WriteLine();
                    Console.WriteLine($"Current Money Provided:".PadRight(40) + "| " + "$" + $"{machine.CurrentMoneyProvided:0.00}");
                    Console.WriteLine();

                }
                string ItemSelection = Console.ReadLine().ToUpper();
                Regex reg = new Regex($"(?:REGEX)\\s((?:\\w+)\\s?(?:\\w+)?)");


                if (machine.Inventory.Keys.Contains(ItemSelection) && machine.Inventory[ItemSelection].Count > 0)
                {
                    Console.Clear();
                    machine.AddItemToCart(machine.Inventory[ItemSelection][0]);
                    machine.RemoveInventory(machine.Inventory[ItemSelection][0]);
                }
                else if (ItemSelection.ToUpper() == "D" || ItemSelection.ToUpper() == "DONE")
                {
                    Console.Clear();
                    PrintPurchaseMenu();
                }
                else if(reg.IsMatch(ItemSelection))
                {
                    machine.RemoveItemsFromCart(machine.Inventory[ItemSelection][0]);
                    machine.ReturnToInventory(machine.Inventory[ItemSelection][0]);
                }
                else
                {

                    Console.Clear();
                    Console.WriteLine($"{ItemSelection} is not a valid choice. Please select one of the values below.");
                    Console.WriteLine();
                }

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
                Console.WriteLine($"Money Inserted: {machine.CurrentMoneyProvided}");
                string answer = Console.ReadLine();

                decimal moneyInserted;
                decimal.TryParse(answer, out moneyInserted);

              
                if (moneyInserted == 1 || moneyInserted == 2 || moneyInserted == 5 ||
                    moneyInserted == 10 || moneyInserted == 20)
                {
                    Console.Clear();
                    machine.AddMoney(moneyInserted);
                    machine.PrintLog("FEED MONEY: $" + moneyInserted + "     $" + machine.CurrentMoneyProvided.ToString());
                }
                else if (answer.ToUpper() == "D" || answer.ToUpper() == "Done" || answer.ToLower() == "D" || answer.ToLower() == "Done")
                {
                    Console.Clear();
                    PrintPurchaseMenu();
                }


                else
                {
                    Console.Clear();
                    Console.WriteLine($"{moneyInserted} is not a valid denomination. Please select one of the values below.");
                    Console.WriteLine();
                }
            }
        }
    }
}
