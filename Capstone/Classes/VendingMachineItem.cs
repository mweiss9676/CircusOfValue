using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    public abstract class VendingMachineItem
    {
        public string NameOfItem { get;  set; }

        public decimal PriceOfItem { get;  set; }

        public string SlotID { get;  set; }

        public abstract string ItemYumYum();

        public VendingMachineItem(string nameOfItem, decimal priceOfItem, string slotID)
        {
            this.NameOfItem = nameOfItem;
            this.PriceOfItem = priceOfItem;
            this.SlotID = slotID;
        }

    }
}
