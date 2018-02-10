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
            test.CalculateTotalShoppingCart(test.ShoppingCart);

            Assert.AreEqual(2.40M, test.TotalCart);
        }
        [TestMethod]
        public void RemoveItemFromCart()
        {
            test.AddItemToCart(chips);
            test.RemoveItemsFromCart(chips);
            test.CalculateTotalShoppingCart(test.ShoppingCart);

            Assert.AreEqual(0, test.TotalCart);
        }
    }
}
