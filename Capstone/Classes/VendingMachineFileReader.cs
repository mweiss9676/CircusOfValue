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
        private const int Default_Quantity = 5;
        private const int Col_SlotId = 0;
        private const int Col_Name = 1;
        private const int Col_Cost = 2;

        public Dictionary<string, List<VendingMachineItem>> GetInventory()
        {
            Regex startsWithA = new Regex(@"^A");
            Regex startsWithB = new Regex(@"^B");
            Regex startsWithC = new Regex(@"^C");
            Regex startsWithD = new Regex(@"^D");

            Dictionary<string, List<VendingMachineItem>> inventory = new Dictionary<string, List<VendingMachineItem>>();

            try
            {
                using (StreamReader sr = new StreamReader(@"Classes/IOFiles/InputInventory.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] eachLineArray = sr.ReadLine().Split('|');

                        string slotID = eachLineArray[Col_SlotId];
                        string nameOfItem = eachLineArray[Col_Name];
                        string priceOfItem = eachLineArray[Col_Cost];

                        List<VendingMachineItem> vendingMachineItems = new List<VendingMachineItem>();
                        for (int i = 0; i < Default_Quantity; i++)
                        {
                            if (startsWithA.IsMatch(slotID))
                            {
                                vendingMachineItems.Add(new ChipItem(nameOfItem, decimal.Parse(priceOfItem), slotID));
                            }
                            else if (startsWithB.IsMatch(slotID))
                            {
                                vendingMachineItems.Add(new CandyItem(nameOfItem, decimal.Parse(priceOfItem), slotID));
                            }
                            else if (startsWithC.IsMatch(slotID))
                            {
                                vendingMachineItems.Add(new DrinkItem(nameOfItem, decimal.Parse(priceOfItem), slotID));
                            }
                            else if (startsWithD.IsMatch(slotID))
                            {
                                vendingMachineItems.Add(new GumItem(nameOfItem, decimal.Parse(priceOfItem), slotID));
                            }
                        }

                        inventory.Add(slotID, vendingMachineItems);
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"That isn't a valid File Path see {e}");
            }

            return inventory;
        }
    }
}
