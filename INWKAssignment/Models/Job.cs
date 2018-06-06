using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INWKAssignment.Models
{
    public class Job
    {
        public int Id { get; set; }
        public List<Item> Items { get; set; }
        public Customer Customer { get; set; }
        public bool HasExtraMargin { get; set; }
        public decimal Total { get; set; }

        public Job()
        {
            this.Items = new List<Item>();
            this.Customer = new Customer();
        }
    }
}