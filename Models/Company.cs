using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_sourcing.Models
{
    public class Company
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Revenue { get; set; } = string.Empty;
        public int Employees { get; set; } = 0;
        public EnumLocation Location { get; set; }
    }
}
