using INWKAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INWKAssignment.ViewModels
{
    public class CalculateJobViewModel
    {
        public Job Job { get; set; }
        public List<ItemType> ItemTypes { get; set; }
        public string TotalRounded { get; set; }
    }
}