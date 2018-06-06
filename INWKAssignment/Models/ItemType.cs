using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INWKAssignment.Models
{
    public class ItemType
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public bool IsExempt { get; set; }
    }
}