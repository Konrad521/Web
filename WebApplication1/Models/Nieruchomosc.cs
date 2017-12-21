using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models


{[Table("Table_2")]
    public class Nieruchomosc
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "ID Osoby")]
        [Required]
        public int IDOsoby {get; set;}
        
        [Display(Name = "Nazwa")]
        public string Nazwa { get; set; }
        
        
        [Display(Name = "Adres")]
        public string Adres { get; set; }

        [Display(Name = "lat")]
        public double lat { get; set; }

        [Display(Name = "lng")]
        public double lng { get; set; }




    }
}