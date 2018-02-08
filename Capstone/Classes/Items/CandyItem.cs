using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes.Items
{
    public class CandyItem : VendingMachineItem
    {
        public CandyItem(string nameOfItem, decimal priceOfItem, string slotID) : base(nameOfItem, priceOfItem, slotID)
        {
            this.NameOfItem = nameOfItem;
            this.PriceOfItem = priceOfItem;
            this.SlotID = slotID;
        }

        public override string ItemYumYum()
        {
            return "Munch Munch, Yum!";
        }
    }
}
