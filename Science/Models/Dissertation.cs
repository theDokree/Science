using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Science.Models
{
    public class Dissertation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int Type { get; set; }
        public string Rank { get; set; }
        public string Index { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public DateTime Date { get; set; }
    }
}