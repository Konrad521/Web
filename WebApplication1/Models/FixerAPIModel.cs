using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class FixerAPIModel
    {
        public string @base { get; set; }
        public DateTime @date { get; set; }
        public Dictionary<string, decimal> rates { get; set; }

        public string bazowa { get { return this.@base; } }
    }
}