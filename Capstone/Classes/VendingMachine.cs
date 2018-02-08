using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    public class VendingMachine
    {
        public Dictionary<VendingMachineItem, int> Inventory { get; set; }

        public decimal Balance { get; private set; }

        public string[] Slots { get; }

        public void FeedMoney (int dollars)
        {

        }

        //public VendingMachineItem GetItemAtSlot (string slot)
        //{

        //}
    }
}
