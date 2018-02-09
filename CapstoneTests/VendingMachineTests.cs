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
        
        VendingMachineLogic test2 = new VendingMachineLogic();
        VendingMachineItem chips = new ChipItem("chips", 2.40M, "A1");
        

        [TestMethod]
        public void AmountOfMoneyAdded_MatchesCurrentMoneyProvided()
        {
            test2.AddMoney(5.00M);
            test2.AddMoney(1.00M);
            test2.AddMoney(10.00M);

            Assert.AreEqual(16.00M, test2.CurrentMoneyProvided);
        }
        [TestMethod]
        public void AddItemsToCart_UpdateTotal()
        {
            test2.AddItemToCart(chips);
            test2.CalculateTotalShoppingCart(;

            Assert.AreEqual(2.40M, test2.TotalCart);
        }
    }
}
