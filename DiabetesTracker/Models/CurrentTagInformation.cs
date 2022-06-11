using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace DiabetesTracker.Models
{
    // POCO class used for data binding
    public class CurrentTagInformation
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public int PostCount { get; set; }
    }
}
