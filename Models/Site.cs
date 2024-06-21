using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_sourcing.Models
{
    public class Site
    {
        public string Url { get; set; } = null!;
        public string NameXPath { get; set; } = null!;
        public string DescriptionXPath { get; set; } = null!;
        public string Location { get; set; } = null!;
    }
}
