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
        public Dictionary<string, List<VendingMachineItem>> Inventory { get; }

        public decimal Balance { get; private set; }

        public string[] Slots { get; }

        public decimal CurrentMoneyProvided { get; private set; }

        public VendingMachineLogic()
        {
            VendingMachineFileReader filereader = new VendingMachineFileReader();
            Inventory = filereader.GetInventory();
        }

        public void CurrentMoney(decimal feedMoney)
        {
            CurrentMoneyProvided += feedMoney;
        }

    }
}
