using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;
using Capstone;
using Capstone.Classes.IOFiles;
using Capstone.Classes.Items;


namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTests
    {
        
        VendingMachineLogic test = new VendingMachineLogic();
        VendingMachineItem chips = new ChipItem("chips", 2.40M, "A1");
        VendingMachineItem drink = new DrinkItem("drink", 1.40M, "D2");
        VendingMachineItem gum = new ChipItem("gum", 3.80M, "C3");
        VendingMachineItem heavy = new ChipItem("Heavy", 5.80M, "C2");
        VendingMachineItem realStats = new DrinkItem("Cola", 1.25M, "C1");


        [TestMethod]
        public void AmountOfMoneyAdded_MatchesCurrentMoneyProvided()
        {
            test.AddMoney(5.00M);
            test.AddMoney(1.00M);
            test.AddMoney(10.00M);

            Assert.AreEqual(16.00M, test.CurrentMoneyProvided);
        }
        [TestMethod]
        public void AddItemsToCart_UpdateTotal()
        {
            test.AddItemToCart(chips);
            test.AddItemToCart(gum);
            test.AddItemToCart(drink);
            test.CalculateTotalShoppingCart(test.ShoppingCart);

            Assert.AreEqual(7.60M, test.TotalCart);
        }

        [TestMethod]
        public void RemoveItemsFromCart()
        {
            test.AddItemToCart(chips);
            test.AddItemToCart(drink);
            test.AddItemToCart(gum);
            test.CalculateTotalShoppingCart(test.ShoppingCart);

            Assert.AreEqual(7.60M, test.TotalCart);

            test.RemoveItemsFromCart(chips);
            test.CalculateTotalShoppingCart(test.ShoppingCart);

            Assert.AreEqual(5.20M, test.TotalCart);

        }

        [TestMethod]
        public void CheckRemovedFromInventory()
        {
            int countBefore = 0;

            foreach (var kvp in test.Inventory)
            {
                countBefore += kvp.Value.Count;
            }

            Assert.AreEqual(80, countBefore);

            test.RemoveItemFromInventory(realStats);

            int countAfter = 0;

            foreach (var kvp in test.Inventory)
            {
                countAfter += kvp.Value.Count;
            }

           
            Assert.AreEqual(79, countAfter);
        }

        [TestMethod]
        public void CoinSound()
        {
            test.AddMoney(2.0M);

            Change change = test.GetChange();

            change.ChangeSound();
        }
    }
}
