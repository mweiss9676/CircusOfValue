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
        static bool soundsOFF = false;

        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Clear();
            Console.SetWindowSize(Console.LargestWindowWidth, 41);
            Console.SetBufferSize(Console.LargestWindowWidth, 80);
            Console.SetWindowPosition(0, 0);

            while (true)
            {
                try
                {
                    TopMenu();
                }
                //Add more specific exception later
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private static void TopMenu()
        {
            Console.WriteLine();
            Console.WriteLine("(1) Display Items");
            Console.WriteLine();
            Console.WriteLine("(2) Purchase Items");
            Console.WriteLine();
            Console.WriteLine("(3) Close Vending Machine");
            Console.WriteLine();
            Console.WriteLine("(4) Sounds Off");
            string mainMenuResult = Console.ReadLine();

            if(mainMenuResult == "1" || mainMenuResult.ToUpper() == "D" || mainMenuResult.ToUpper() == "DISPLAY")
            {
                Console.Clear();
                DisplayMenu();
            }
            else if(mainMenuResult == "2" || mainMenuResult.ToUpper() == "P" || mainMenuResult.ToUpper() == "PURCHASE")
            {
                Console.Clear();
                MainMenu();
            }
            else if (mainMenuResult == "3" || mainMenuResult.ToUpper() == "C" || mainMenuResult.ToUpper() == "CLOSE")
            {
                Console.Clear();
                Environment.Exit(0);
            }
            else if (mainMenuResult == "4" || mainMenuResult.ToUpper() == "S" 
                  || mainMenuResult.ToUpper() == "OFF" || mainMenuResult.ToUpper() == "O"
                  || mainMenuResult.ToUpper() == "SOUNDS")
            {
                Console.Clear();
                soundsOFF = true;
                Console.WriteLine("Turning Sounds Off...");
                Console.WriteLine();
            }
            else if (mainMenuResult == "45")
            {
                Console.Clear();
                Console.WriteLine("ADMIN ONLY: ENTER PASSWORD:");
                string password = Console.ReadLine();
                if (password == "No Joshes Allowed")
                {
                    Console.Clear();
                    AdminMenu();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Nice Try Josh");
                    Console.WriteLine();
                    TopMenu();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"{mainMenuResult} is not a valid option, please select from the available choices.");
                Console.WriteLine();
            }
        }

        private static void DisplayMenu()
        {
            Console.WriteLine();
            foreach (var kvp in machine.Inventory)
            {
                if (kvp.Value.Count <= 0)
                {
                    Console.WriteLine("SOLD OUT");
                }
                else
                {
                    Console.WriteLine($"{kvp.Key} | {kvp.Value[0].NameOfItem} | {kvp.Value[0].PriceOfItem}");
                }
            }
            Console.WriteLine();
        }

        private static void MainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("(1) Insert Money");
            Console.WriteLine();
            Console.WriteLine("(2) Select Product");
            Console.WriteLine();
            Console.WriteLine("(3) Finish Transaction");
            Console.WriteLine();
            Console.WriteLine("(4) Close Vending Machine");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Current Money Provided: ${machine.CurrentMoneyProvided} ");
            string purchaseMenuResult = Console.ReadLine();

            if (purchaseMenuResult == "1" || purchaseMenuResult.ToUpper() == "INSERT" || purchaseMenuResult.ToUpper() == "I")
            {
                Console.Clear();
                FeedMoneyMenu();
            }
            else if (purchaseMenuResult == "2" || purchaseMenuResult.ToUpper() == "SELECT" || purchaseMenuResult.ToUpper() == "S")
            {
                Console.Clear();
                ShoppingCartMenu();
            }
            else if (purchaseMenuResult == "3" || purchaseMenuResult.ToUpper() == "FINISH" || purchaseMenuResult.ToUpper() == "F")
            {
                Console.Clear();
                machine.CalculateTotalShoppingCart(machine.ShoppingCart);
                CompleteTransactionMenu();
            }
            else if (purchaseMenuResult == "4" || purchaseMenuResult.ToUpper() == "CLOSE" || purchaseMenuResult.ToUpper() == "C")
            {
                Console.Clear();
                Environment.Exit(0);
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"You Entered {purchaseMenuResult}. This Is Not A Valid Option.");
                Console.WriteLine();
                MainMenu();
            }
        }

        private static void FeedMoneyMenu()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Please Insert Money:");
                Console.WriteLine("(1)  $1");
                Console.WriteLine("(2)  $2");
                Console.WriteLine("(5)  $5");
                Console.WriteLine("(10) $10");
                Console.WriteLine("(20) $20");
                Console.WriteLine("(D)one inserting money.");
                Console.WriteLine();
                Console.WriteLine($"Money Inserted: ${machine.CurrentMoneyProvided}");
                Console.WriteLine();
                if (machine.ShoppingCart.Count > 0)
                {
                    Console.WriteLine($"Current Total: ${machine.TotalCart}");
                }
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
                    MainMenu();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"{moneyInserted} is not a valid denomination. Please select one of the values below.");
                    Console.WriteLine();
                }
            }
        }

        private static void ShoppingCartMenu()
        {
            while (true)
            {
                Console.WriteLine();
                foreach (var kvp in machine.Inventory)
                {
                    if (machine.GetEachItemsCurrentInventory(kvp.Value) > 0)
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
                Regex reg = new Regex($"^(?:REMOVE)\\s((?:\\w+)\\s?(?:\\w+)?)");
                Match match = Regex.Match(ItemSelection, $"(?<=REMOVE\\s)((\\w+)\\s?(\\w+)?)$");

                if (machine.Inventory.Keys.Contains(ItemSelection) && machine.Inventory[ItemSelection].Count > 0)
                {
                    Console.Clear();
                    machine.AddItemToCart(machine.Inventory[ItemSelection][0]);
                    machine.RemoveItemFromInventory(machine.Inventory[ItemSelection][0]);
                }
                else if (ItemSelection.ToUpper() == "D" || ItemSelection.ToUpper() == "DONE")
                {
                    Console.Clear();
                    MainMenu();
                }
                else if (reg.IsMatch(ItemSelection) && machine.ShoppingCart.Count > 0
                    && machine.ShoppingCart.Any(x => x.NameOfItem.ToUpper() == match.Groups[1].ToString())) //checks to make sure mispellings aren't searched for
                {
                    //Gets the key from the slotID property of the shopping cart so that it can be passed to the methods below.
                    var key = (from k in machine.ShoppingCart
                               where string.Compare(k.NameOfItem, match.Groups[1].ToString(), true) == 0
                               select k.SlotID).FirstOrDefault();

                    var item = machine.ShoppingCart.FindIndex(x => x.NameOfItem.ToUpper() == match.Value);

                    Console.Clear();
                    machine.ReturnToInventory(machine.ShoppingCart[item]);
                    machine.RemoveItemsFromCart(machine.ShoppingCart[item]);
                }
                else if (reg.IsMatch(ItemSelection) && machine.ShoppingCart.Count > 0
                   && machine.ShoppingCart.Any(x => x.SlotID.ToUpper() == match.Groups[1].ToString())) //checks to make sure mispellings aren't searched for
                {
                    var item = machine.ShoppingCart.FindIndex(x => x.SlotID.ToUpper() == match.Value);

                    Console.Clear();
                    machine.ReturnToInventory(machine.ShoppingCart[item]);
                    machine.RemoveItemsFromCart(machine.ShoppingCart[item]);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"{ItemSelection} is not a valid choice. Please select one of the values below.");
                    Console.WriteLine();
                }
            }
        }

        private static void CompleteTransactionMenu()
        {
            Console.WriteLine();
            Console.WriteLine($"Your Total Cart is: ${machine.TotalCart}");
            Console.WriteLine();
            Console.WriteLine($"Your Current Money Inserted is: ${machine.CurrentMoneyProvided}");
            Console.WriteLine();
            Console.WriteLine("Are You Ready To Complete The Transaction?");
            Console.WriteLine("(1) Yes");
            Console.WriteLine("(2) No");
            string completeTransaction = Console.ReadLine();
            
            if (completeTransaction.ToUpper() == "1" || completeTransaction.ToUpper() == "Y" || completeTransaction.ToUpper() == "YES")
            {
                if (machine.CurrentMoneyProvided - machine.TotalCart >= 0)
                {
                    Console.Clear();
                    TransactionCompletedGiveChangeMenu();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Insufficient Funds, Please Insert More Money");
                    Console.WriteLine();
                    MainMenu();
                }
            }
            else if (completeTransaction.ToUpper() == "2" || completeTransaction.ToUpper() == "N" || completeTransaction.ToUpper() == "NO")
            {
                Console.Clear();
                MainMenu();
            }
        }

        private static void TransactionCompletedGiveChangeMenu()
        {
            machine.UpdateSalesReport(machine.Inventory);
            Change change = machine.GetChange();
            Console.WriteLine();
            Console.WriteLine($"Total Change: ${change.TotalChange}");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine();
            Console.WriteLine($"Quarter(s): {change.Quarters}");
            Console.WriteLine($"Dime(s): {change.Dimes}");
            Console.WriteLine($"Nickel(s): {change.Nickels}");
            Console.WriteLine();

            foreach (var item in machine.ShoppingCart)
            {
                if (item.SlotID.Contains("A"))
                {
                    Console.WriteLine($"You are crunching on {item.NameOfItem} {item.ItemYumYum()}");
                }
                else if (item.SlotID.Contains("B"))
                {
                    Console.WriteLine($"You are munching on {item.NameOfItem} {item.ItemYumYum()}");
                }
                else if (item.SlotID.Contains("C"))
                {
                    Console.WriteLine($"You are drinking {item.NameOfItem} {item.ItemYumYum()}");
                }
                else
                {
                    Console.WriteLine($"You are chewing on {item.NameOfItem} {item.ItemYumYum()}");
                }
            }

            while (machine.ShoppingCart.Count > 0)
            {
                machine.PrintLog($"{machine.ShoppingCart[0].NameOfItem}" +
                                $" {machine.ShoppingCart[0].SlotID}" +
                                $" ${machine.CurrentMoneyProvided}" +
                                $" ${(machine.CurrentMoneyProvided - machine.ShoppingCart[0].PriceOfItem)}");
                machine.PayForItem(machine.ShoppingCart[0]);
                machine.RemoveItemsFromCart(machine.ShoppingCart[0]);
            }
            

            machine.CalculateTotalShoppingCart(machine.ShoppingCart);

            machine.ResetCurrentMoneyProvided();

            machine.PrintLog($"GIVE CHANGE: ${change.TotalChange}    ${machine.CurrentMoneyProvided:0.00}");

            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("(1) Return To Main Menu");
                Console.WriteLine("(2) Close Vending Machine");

                if (soundsOFF == false)
                {
                    change.ChangeSound();
                }

                string menuChoice = Console.ReadLine();

                if (menuChoice.ToUpper() == "1" || menuChoice.ToUpper() == "RETURN" || menuChoice.ToUpper() == "R")
                {
                    Console.Clear();
                    TopMenu();
                }
                else if (menuChoice.ToUpper() == "2" || menuChoice.ToUpper() == "CLOSE" || menuChoice.ToUpper() == "C")
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("That Is Not A Valid Choice, Please Enter (1) Or (2)");
                }
            }
        }

        private static void AdminMenu()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("(1) Exit Admin Menu");
                Console.WriteLine();
                Console.WriteLine("(2) Open Report");
                Console.WriteLine();
                Console.WriteLine("(3) Print Sales Report");
                Console.WriteLine();
                string answer = Console.ReadLine();

                if (answer == "1" || answer.ToUpper() == "E" || answer.ToUpper() == "EXIT")
                {
                    Console.Clear();
                    TopMenu();
                }
                else if (answer == "2" || answer.ToUpper() == "O" || answer.ToUpper() == "OPEN")
                {
                    Console.Clear();
                    OpenFileMenu();
                }
                else if (answer == "3" || answer.ToUpper() == "P" || answer.ToUpper() == "PRINT")
                {
                    Console.Clear();
                    Console.WriteLine("Printing Sales Report...");
                    Console.WriteLine();
                    machine.PrintSalesReport();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"{answer} Is Not A Valid Option, Please Select From One Of The Options Below");
                    Console.WriteLine();
                }
            }
        }

        private static void OpenFileMenu()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("What Report Would You Like To Open");
                Console.WriteLine();
                Console.WriteLine("(1) Open Sales Report");
                Console.WriteLine();
                Console.WriteLine("(2) Open Transaction Log");
                Console.WriteLine();
                Console.WriteLine("(3) Return To Main Menu");
                Console.WriteLine();
                Console.WriteLine("(4) Exit The Application");
                Console.WriteLine();
                string openMenuResponse = Console.ReadLine();

                if (openMenuResponse == "1" || openMenuResponse.ToUpper() == "SALES" || openMenuResponse.ToUpper() == "S")
                {
                    Console.Clear();
                    machine.OpenReport(@"Sales_Report.txt");
                }
                else if (openMenuResponse == "2" || openMenuResponse.ToUpper() == "TRANSACTION" || openMenuResponse.ToUpper() == "T")
                {
                    Console.Clear();
                    machine.OpenReport(@"Log.txt");
                }
                else if (openMenuResponse == "3" || openMenuResponse.ToUpper() == "RETURN" || openMenuResponse.ToUpper() == "R")
                {
                    Console.Clear();
                    TopMenu();
                }
                else if (openMenuResponse == "4" || openMenuResponse.ToUpper() == "EXIT" || openMenuResponse.ToUpper() == "E")
                {
                    Console.Clear();
                    Environment.Exit(0);                    
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"{openMenuResponse} Is Not A Valid Choice, Please Select From The Options Below");
                }
            }
        }
    }
}
