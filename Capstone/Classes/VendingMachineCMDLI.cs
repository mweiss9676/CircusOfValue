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
            ConsoleColor primaryColor = ConsoleColor.Red;
            ConsoleColor secondaryColor = ConsoleColor.White;
            Console.BackgroundColor = secondaryColor;
            Console.ForegroundColor = primaryColor;
            Console.Clear();
            Console.SetWindowSize(Console.LargestWindowWidth, 41);
            Console.SetBufferSize(Console.LargestWindowWidth * 2, 100);
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
            CircusOf();

            string[] menu = { "(1) Display Items", "(2) Purchase Items", "(3) Close Vending Machine", "(4) Sounds Off" };
            PrintMenus(menu);

            Value();

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
                CircusOf();
                string[] soundsOff = { "Turning Sounds Off...", "Press Return To Return"};
                PrintMenus(soundsOff);
                Value();
                Console.ReadLine();
                Console.Clear();

            }
            else if (mainMenuResult == "45")
            {
                Console.Clear();
                CircusOf();
                string[] joshMenu = { "ADMIN ONLY: ENTER PASSWORD:" };
                PrintMenus(joshMenu);
                Value();
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
            while (true)
            {
                CircusOfSmall();

                Console.WriteLine();
                foreach (var kvp in machine.Inventory)
                {
                    Console.SetCursorPosition((Console.WindowWidth - 20) / 2, Console.CursorTop);
                    if (kvp.Value.Count <= 0)
                    {
                        Console.WriteLine("SOLD OUT");
                    }
                    else
                    {
                        Console.WriteLine($"{kvp.Key} | {kvp.Value[0].NameOfItem.PadRight(20)} | ${kvp.Value[0].PriceOfItem}");
                    }
                }
                Console.WriteLine();
                Console.SetCursorPosition((Console.WindowWidth - 20) / 2, Console.CursorTop);
                Console.WriteLine("Press Return To Return");
                Value();
                Console.ReadLine();
                Console.Clear();
                TopMenu();
            }
        }

        private static void MainMenu()
        {
            CircusOf();
            string[] menu = { "(1) Insert Money", "(2) Select Product", "(3) Finish Transaction", "(4) Close Vending Machine", $"Current Money Provided: ${machine.CurrentMoneyProvided} " };
            PrintMenus(menu);

            Value();
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
                string[] invalid = { $"You Entered {purchaseMenuResult}. This Is Not A Valid Option." };
                PrintMenus(invalid);
                MainMenu();
            }
        }

        private static void FeedMoneyMenu()
        {
            while (true)
            {
                CircusOfSmall();
                string[] menu = { "Please Insert Money:", "(1)  $1", "(2)  $2", "(5)  $5", "(10) $10", "(20) $20", "(D)one Inserting Money", $"Money Inserted: ${machine.CurrentMoneyProvided}" };
                int longest = menu.Max(x => x.Length);

                PrintMenus(menu);
                

                if (machine.ShoppingCart.Count > 0)
                {
                    Console.SetCursorPosition((Console.WindowWidth - longest) / 2, Console.CursorTop);

                    Console.WriteLine($"Current Total: ${machine.TotalCart}");
                }
                Value();
                string answer = Console.ReadLine();

                decimal moneyInserted;
                decimal.TryParse(answer, out moneyInserted);


                if (moneyInserted == 1 || moneyInserted == 2 || moneyInserted == 5 ||
                    moneyInserted == 10 || moneyInserted == 20)
                {
                    Console.Clear();
                    machine.AddMoney(moneyInserted);
                    //machine.PrintLog("FEED MONEY: $" + moneyInserted + "     $" + machine.CurrentMoneyProvided.ToString());
                    machine.PrintLog($"FEED MONEY:".PadRight(23) + $"{("$" + moneyInserted)}".PadRight(8) + $"{("$" + machine.CurrentMoneyProvided)}");
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
                //Console.Clear();
                CircusOfSmall();
                List<string> concatMenu = new List<string>();
                int sizeOfCart = machine.ShoppingCart.Count;

                foreach (var kvp in machine.Inventory)
                {
                    if (machine.GetEachItemsCurrentInventory(kvp.Value) > 0)
                    {
                        concatMenu.Add($"{kvp.Key.PadRight(2).PadLeft(10)} | {kvp.Value[0].NameOfItem.PadRight(20)} | ${kvp.Value[0].PriceOfItem}");
                    }
                    else
                    {
                        concatMenu.Add("SOLD OUT".PadLeft(41));
                    }
                }

                if (sizeOfCart > 0 && sizeOfCart < 17)
                {
                    string name = machine.ShoppingCart[0].NameOfItem;
                    string price = machine.ShoppingCart[0].PriceOfItem.ToString();
                    concatMenu[0] += "Your Current Cart: ".PadLeft(22) + name + ("$" + price).PadLeft(10);

                    for (int i = 1; i < sizeOfCart; i++)
                    {
                        string nameOfItem = machine.ShoppingCart[i].NameOfItem;
                        string priceOfItem = machine.ShoppingCart[i].PriceOfItem.ToString();
                        concatMenu[i] += nameOfItem.PadLeft(35) + ("$" + priceOfItem).PadLeft(10);
                    }
                }

                else if (sizeOfCart >= 17 && sizeOfCart <= 32)
                {
                    for (int i = 17; i < sizeOfCart + 1; i++)
                    {
                        string nameOfItem = machine.ShoppingCart[i - 1].NameOfItem;
                        string priceOfItem = machine.ShoppingCart[i - 1].PriceOfItem.ToString();
                        concatMenu[i - 17] += nameOfItem.PadLeft(35) + ("$" + priceOfItem).PadLeft(10);
                    }
                }
                else if (sizeOfCart >= 33 && sizeOfCart <= 48)
                {
                    for (int i = 33; i < sizeOfCart + 1; i++)
                    {
                        string nameOfItem = machine.ShoppingCart[i - 1].NameOfItem;
                        string priceOfItem = machine.ShoppingCart[i - 1].PriceOfItem.ToString();
                        concatMenu[i - 33] += nameOfItem.PadLeft(35) + ("$" + priceOfItem).PadLeft(10);
                    }
                }
                else if (sizeOfCart >= 49 && sizeOfCart <= 64)
                {
                    for (int i = 49; i < sizeOfCart + 1; i++)
                    {
                        string nameOfItem = machine.ShoppingCart[i - 1].NameOfItem;
                        string priceOfItem = machine.ShoppingCart[i - 1].PriceOfItem.ToString();
                        concatMenu[i - 49] += nameOfItem.PadLeft(35) + ("$" + priceOfItem).PadLeft(10);
                    }
                }
                else if (sizeOfCart >= 65 && sizeOfCart <= 80)
                {
                    for (int i = 65; i < sizeOfCart + 1; i++)
                    {
                        string nameOfItem = machine.ShoppingCart[i - 1].NameOfItem;
                        string priceOfItem = machine.ShoppingCart[i - 1].PriceOfItem.ToString();
                        concatMenu[i - 65] += nameOfItem.PadLeft(35) + ("$" + priceOfItem).PadLeft(10);
                    }
                }
                
                machine.CalculateTotalShoppingCart(machine.ShoppingCart);
                string totalCart = machine.TotalCart.ToString();
                string currentMoney = machine.CurrentMoneyProvided.ToString();
                concatMenu.Add("---------------------------------".PadLeft(41));
                concatMenu.Add("Total: | $".PadLeft(37) + totalCart);
                concatMenu.Add("Current Money Provided: | $".PadLeft(37) + currentMoney.PadRight(8));

                PrintConcatenatedMenu(concatMenu);
                Console.WriteLine("(D)one shopping?".PadLeft(25));
                Console.WriteLine();
                Console.WriteLine("Remove selection? (ex. Remove Potato Crisps)".PadLeft(54));

                ValueSmall();
                string ItemSelection = Console.ReadLine().ToUpper();
                Regex reg = new Regex($"^(?:REMOVE)\\s((?:\\w+)\\s?(?:\\w+)?)");
                Match match = Regex.Match(ItemSelection, $"(?<=REMOVE\\s)((\\w+)\\s?(\\w+)?)$");

                //Adds the customer's choice to their cart and removes it from the inventory
                if (machine.Inventory.Keys.Contains(ItemSelection) && machine.Inventory[ItemSelection].Count > 0)
                {
                    Console.Clear();
                    machine.AddItemToCart(machine.Inventory[ItemSelection][0]);
                    machine.RemoveItemFromInventory(machine.Inventory[ItemSelection][0]);
                }

                //returns to the main menu
                else if (ItemSelection.ToUpper() == "D" || ItemSelection.ToUpper() == "DONE")
                {
                    Console.Clear();
                    MainMenu();
                }

                //removes item from cart by matching with the item's Name
                else if (reg.IsMatch(ItemSelection) && machine.ShoppingCart.Count > 0
                    && machine.ShoppingCart.Any(x => x.NameOfItem.ToUpper() == match.Groups[1].ToString())) //checks to make sure mispellings aren't searched for
                {
                    var item = machine.ShoppingCart.FindIndex(x => x.NameOfItem.ToUpper() == match.Value);

                    Console.Clear();
                    machine.ReturnToInventory(machine.ShoppingCart[item]);
                    machine.RemoveItemsFromCart(machine.ShoppingCart[item]);
                }

                //removes the item from the cart if it matches with item's SlotID
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
                    PrintMenusSingleSpaced(new string[] { $"{ItemSelection} is not a valid choice. Please select one of the values below." });
                }
            }
        }

        private static void CompleteTransactionMenu()
        {
            string totalCart = "Your Total Cart is:" + machine.TotalCart.ToString();
            string currentMoney = "Your Current Money Inserted is: " + machine.CurrentMoneyProvided.ToString();

            string[] menu = { totalCart, currentMoney, "Are You Ready To Complete The Transaction?", "(1) Yes", "(2) No" };
            CircusOf();
            PrintMenus(menu);
            Value();

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
                    PrintMenus(new string[] { "Insufficient Funds, Come Back When You Have Some Currency Fella!" });
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
            Change change = machine.GetChange();
            string totalChange = "Total Change: " + change.TotalChange.ToString();
            string quarters = "Quarter(s):" + change.Quarters.ToString();
            string dimes = "Dime(s):" + change.Dimes.ToString();
            string nickels = "Nickel(s):" + change.Nickels.ToString();
            machine.UpdateSalesReport(machine.Inventory);

            string[] menu = { totalChange, "---------------------------------------" , quarters, dimes, nickels };


            CircusOf();

            PrintMenus(menu);

            Value();

            if (soundsOFF == false)
            {
                change.ChangeSound();
            }
            Console.ReadLine();

            List<string> temp = new List<string>();

            foreach (var item in machine.ShoppingCart)
            {
                if (item.SlotID.Contains("A"))
                {
                    temp.Add($"You are crunching on {item.NameOfItem} {item.ItemYumYum()}");
                }
                else if (item.SlotID.Contains("B"))
                {
                    temp.Add($"You are munching on {item.NameOfItem} {item.ItemYumYum()}");
                }
                else if (item.SlotID.Contains("C"))
                {
                    temp.Add($"You are drinking {item.NameOfItem} {item.ItemYumYum()}");
                }
                else
                {
                    temp.Add($"You are chewing on {item.NameOfItem} {item.ItemYumYum()}");
                }
            }

            Console.Clear();
            CircusOfSmall();
            PrintMenusSingleSpaced(temp.ToArray());
            ValueSmall();
            Console.ReadLine();


            while (machine.ShoppingCart.Count > 0)
            {
                string name = machine.ShoppingCart[0].NameOfItem;
                string slot = machine.ShoppingCart[0].SlotID;
                string cmp = "$" + machine.CurrentMoneyProvided.ToString();
                string total = "$" + (machine.CurrentMoneyProvided - machine.ShoppingCart[0].PriceOfItem).ToString();

                machine.PrintLog(name.PadRight(20) + slot.PadRight(3) + cmp.PadRight(8) + total);
                machine.PayForItem(machine.ShoppingCart[0]);
                machine.RemoveItemsFromCart(machine.ShoppingCart[0]);
            }
            

            machine.CalculateTotalShoppingCart(machine.ShoppingCart);

            machine.ResetCurrentMoneyProvided();

            machine.PrintLog($"GIVE CHANGE:".PadRight(23) +  $"${change.TotalChange}   ${machine.CurrentMoneyProvided:0.00}");

            Console.WriteLine();
            Console.Clear();

            while (true)
            {
                CircusOf();
                string[] menu2 = { "(1) Return To Main Menu", "(2) Close Vending Machine" };
                PrintMenus(menu2);
                Value();

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
                    PrintMenus(new string[] { "That Is Not A Valid Choice, Please Enter (1) Or (2)" });
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

        private static void CircusOf()
        {
            Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            Console.WriteLine("$$     $$$$$$$$$$$$     $$$$$$$$$$$$$$$$$$   $$$$$$$$$$$$         $$$$$$$$$$$$     $$             $$      $$$$$$$$$$                $$$$$$$$$$$$     $$$$$$$$$$$$$$$$    $$");
            Console.WriteLine("$$   $$            $$           $$           $$          $$     $$            $$   $$             $$    $$          $$            $$            $$   $$                  $$");
            Console.WriteLine("$$  $$              $$          $$           $$           $$   $$              $$  $$             $$   $$                        $$              $$  $$                  $$");
            Console.WriteLine("$$  $$                          $$           $$           $$   $$                  $$             $$    $$                       $$              $$  $$                  $$");
            Console.WriteLine("$$  $$                          $$           $$          $$    $$                  $$             $$      $$$$$$$$$$             $$              $$  $$$$$$$$$$$$$$      $$");
            Console.WriteLine("$$  $$                          $$           $$$$$$$$$$$$      $$                  $$             $$               $$$           $$              $$  $$                  $$");
            Console.WriteLine("$$  $$                          $$           $$         $$     $$                  $$             $$                 $$          $$              $$  $$                  $$");
            Console.WriteLine("$$  $$              $$          $$           $$          $$    $$              $$  $$             $$   $             $$          $$              $$  $$                  $$");
            Console.WriteLine("$$   $$            $$           $$           $$           $$    $$            $$    $$           $$     $$          $$            $$            $$   $$                  $$");
            Console.WriteLine("$$     $$$$$$$$$$$$      $$$$$$$$$$$$$$$$$$  $$            $$     $$$$$$$$$$$$        $$$$$$$$$$$         $$$$$$$$$$                $$$$$$$$$$$$     $$                  $$");
            Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
           
        }

        private static void Value()
        {
            Console.WriteLine();
            Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            Console.WriteLine("$$                    $$                $$   $$$$             $$                 $$             $$   $$$$$$$$$$$$$$$$$    $$$$  $$$$  $$$$                               $$");
            Console.WriteLine("$$                     $$              $$   $$   $$           $$                 $$             $$   $$                   $$$$  $$$$  $$$$                               $$");
            Console.WriteLine("$$                      $$            $$    $$    $$          $$                 $$             $$   $$                   $$$$  $$$$  $$$$                               $$");
            Console.WriteLine("$$                       $$          $$     $$     $$         $$                 $$             $$   $$                    $$    $$    $$                                $$");
            Console.WriteLine("$$                        $$        $$      $$      $$        $$                 $$             $$   $$$$$$$$$$$$$$$$$     $$    $$    $$                                $$");
            Console.WriteLine("$$                         $$      $$       $$$$$$$$$$$       $$                 $$             $$   $$                    $$    $$    $$                                $$");
            Console.WriteLine("$$                          $$    $$        $$         $$     $$                 $$             $$   $$                    $$    $$    $$                                $$");
            Console.WriteLine("$$                           $$  $$         $$          $$    $$                 $$             $$   $$                    $$    $$    $$                                $$");
            Console.WriteLine("$$                            $$$$          $$           $$   $$                  $$           $$    $$                                                                  $$");
            Console.WriteLine("$$                             $$           $$            $$  $$$$$$$$$$$$$$$$$$    $$$$$$$$$$$      $$$$$$$$$$$$$$$$$     $$    $$    $$                                $$");
            Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
        }

        private static void PrintMenus(string[] menu)
        {
            int longest = menu.Max(x => x.Length);

            Console.WriteLine();
            foreach (string s in menu)
            {
                Console.SetCursorPosition((Console.WindowWidth - longest) / 2, Console.CursorTop);
                Console.WriteLine(s);
                Console.WriteLine();
            }
        }

        private static void PrintMenusSingleSpaced(string[] menu)
        {
            int longest = menu.Max(x => x.Length);

            foreach (string s in menu)
            {
                Console.SetCursorPosition((Console.WindowWidth - longest) / 2, Console.CursorTop);
                Console.WriteLine(s);
            }
        }

        private static void PrintMenusLeftAligned(string[] menu)
        {
            foreach (string s in menu)
            {
                Console.SetCursorPosition((Console.CursorLeft / 3), Console.CursorTop);
                Console.WriteLine(s);
            }
        }

        private static void PrintConcatenatedMenu(List<string> menu)
        {
            foreach (string s in menu)
            {
                Console.SetCursorPosition((Console.CursorLeft), Console.CursorTop);
                Console.WriteLine(s);
            }
        }

        private static void CircusOfSmall()
        {
            Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            Console.WriteLine("$$       $$$$$$$$$$$$       $$$$$$$$$$$$$$     $$$$$$$$$$$$         $$$$$$$$$$$$     $$             $$     $$$$$$$$$$$$                 $$$$$$$$$$$     $$$$$$$$$$$$     $$");
            Console.WriteLine("$$     $$            $$           $$           $$          $$     $$            $    $$             $$    $$                          $$           $$   $$               $$");
            Console.WriteLine("$$    $$                          $$           $$$$$$$$$$$$      $$                  $$             $$     $$$$$$$$$$$               $$             $$  $$$$$$$$         $$");
            Console.WriteLine("$$    $$                          $$           $$          $$    $$                  $$             $$                $$             $$             $$  $$               $$");
            Console.WriteLine("$$     $$            $$           $$           $$           $$    $$            $     $$           $$     $           $$              $$           $$   $$               $$");
            Console.WriteLine("$$       $$$$$$$$$$$$       $$$$$$$$$$$$$$     $$            $$     $$$$$$$$$$$$        $$$$$$$$$$$        $$$$$$$$$$$$                 $$$$$$$$$$$     $$               $$");
            Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
        }

        private static void ValueSmall()
        {
            Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            Console.WriteLine("$$                         $$             $$   $$$$          $$            $$             $$   $$$$$$$$$$$$$$$$$    $$$$   $$$$   $$$$   $$$$                             $$");
            Console.WriteLine("$$                          $$          $$     $$  $$        $$            $$             $$   $$                   $$$$   $$$$   $$$$   $$$$                             $$");
            Console.WriteLine("$$                            $$       $$      $$   $$       $$            $$             $$   $$                   $$$$   $$$$   $$$$   $$$$                             $$");
            Console.WriteLine("$$                             $$    $$        $$$$$$$$      $$            $$             $$   $$$$$$$$$$$$          $$     $$     $$     $$                              $$");
            Console.WriteLine("$$                              $$  $$         $$      $$    $$            $$             $$   $$                    $$     $$     $$     $$                              $$");
            Console.WriteLine("$$                               $$$$          $$       $$   $$             $$           $$    $$                                                                         $$");
            Console.WriteLine("$$                                $$           $$        $$  $$$$$$$$$$$$     $$$$$$$$$$$      $$$$$$$$$$$$$$$$$     $$     $$     $$     $$                              $$");
            Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
        }
    }
}
