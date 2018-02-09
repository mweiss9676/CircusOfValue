using System;
using System.Collections.Generic;
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
        }

        public decimal TotalCart { get; private set; }

        public Dictionary<string, List<VendingMachineItem>> Inventory { get; private set; }

        public List<VendingMachineItem> ShoppingCart = new List<VendingMachineItem>();

        public string[] Slots { get; }

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

        public void ResetCurrentMoneyProvided()
        {
            CurrentMoneyProvided = 0;
        }

        public void RemoveInventory(VendingMachineItem item)
        {
            Inventory[item.SlotID].Remove(item);
            //Inventory.Remove(item.SlotID);
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

        //public void CurrentMoney(decimal feedMoney)
        //{
        //    CurrentMoneyProvided += feedMoney;
        //}

        public Change GetChange()
        {
            Change changeBack = new Change();
            changeBack.CalculateChange(CurrentMoneyProvided, TotalCart);
            return changeBack;
        }

    }
}
