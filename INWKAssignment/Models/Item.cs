using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INWKAssignment.Models
{
    public class Item
    {
        public int Id { get; set; }
        public ItemType ItemType { get; set; }
        public byte ItemTypeId { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxAmount { get; set; }

        public Item()
        {
            this.ItemType = new ItemType();
        }
    }
}