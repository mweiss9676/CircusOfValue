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

                        string slotID = eachLineArray[0];
                        string nameOfItem = eachLineArray[1];
                        string priceOfItem = eachLineArray[2];

                        List<VendingMachineItem> vendingMachineItems = new List<VendingMachineItem>();
                        for (int i = 0; i < 5; i++)
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
