using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class BitBayAPIModel
    {
        public List<decimal[]> bids { get; set; }
        public List<decimal[]> asks { get; set; }

    }
}