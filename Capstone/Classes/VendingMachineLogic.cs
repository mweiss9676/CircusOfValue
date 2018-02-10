using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Classes;

namespace Capstone
{
    public class VendingMachineLogic
    {
        public VendingMachineLogic()
        {
            VendingMachineFileReader filereader = new VendingMachineFileReader();
            Inventory = filereader.GetInventory();
            foreach (var kvp in Inventory)
            {
                NamesOfItems.Add(kvp.Value[0].NameOfItem);
            }
        }

        private List<string> NamesOfItems = new List<string>();

        public decimal TotalCart { get; private set; }

        public Dictionary<string, List<VendingMachineItem>> Inventory { get; private set; }

        private Dictionary<string, int> SalesReportDictionary = new Dictionary<string, int>();

        public List<VendingMachineItem> ShoppingCart = new List<VendingMachineItem>();

        public decimal CurrentMoneyProvided { get; private set; }

        public int GetCurrentInventory(List<VendingMachineItem> eachItemsList)
        {
            int result = 0;

            result = eachItemsList.Count;

            return result;
        }

        public void AddMoney(decimal moneyInserted)
        {
            CurrentMoneyProvided += moneyInserted;
        }

        public void PayForItem(VendingMachineItem item)
        {
            CurrentMoneyProvided -= item.PriceOfItem;
        }

        public void ResetCurrentMoneyProvided()
        {
            CurrentMoneyProvided = 0;
        }

        public void RemoveInventory(VendingMachineItem item)
        {
            Inventory[item.SlotID].Remove(item);
        }
        public void ReturnToInventory(VendingMachineItem item)
        {
            Inventory[item.SlotID].Add(item);
        }

        public void CalculateTotalShoppingCart(List<VendingMachineItem> cart)
        {
            decimal total = 0.0M;

            foreach (VendingMachineItem item in cart)
            {
                total += item.PriceOfItem;
            }

            TotalCart = total;
        }

        public void AddItemToCart(VendingMachineItem item)
        {
            ShoppingCart.Add(item);
        }

        public void RemoveItemsFromCart(VendingMachineItem item)
        {
            ShoppingCart.Remove(item);
        }

        public Change GetChange()
        {
            Change changeBack = new Change();
            changeBack.CalculateChange(CurrentMoneyProvided, TotalCart);
            return changeBack;
        }

        public void PrintLog(string transactionInformation)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("Log.txt", true))
                {
                    DateTime currentDateTime = DateTime.Now;
                    sw.WriteLine(currentDateTime.ToShortDateString() + " " + currentDateTime.ToLongTimeString()
                        + " " + transactionInformation);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void UpdateSalesReport(Dictionary<string, List<VendingMachineItem>> inventory)
        {


            int i = 0;

            foreach (var kvp2 in inventory)
            {
                string name = NamesOfItems[i];
                i++;
                int number = kvp2.Value.Count;
                if (SalesReportDictionary.ContainsKey(name))
                {
                    SalesReportDictionary[name] += 5 - number;
                }
                else
                {
                    SalesReportDictionary.Add(name, 5 - number);
                }
            }
            

        }

        public void PrintSalesReport()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("Sales_Report.txt", true))
                {
                    foreach (var kvp in SalesReportDictionary)
                    {
                        sw.WriteLine($"{kvp.Key}|{kvp.Value}");
                    }
                    sw.WriteLine("---------------------------------");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
