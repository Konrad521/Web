using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
   
        [Table("Table_1")]
        public class Osoba
        {
            public Osoba()
            {
            }

            public Osoba(string imie, string nazwisko, int wiek)
            {
                Imie = imie;
                Nazwisko = nazwisko;
                Wiek = wiek;
            }

            [Key]
            public int ID { get; set; }
            [Display(Name ="Imie")]
            [Required]
            public string Imie { get; set; }
            [Display(Name = "Nazwisko")]
            [Required]
            public string Nazwisko { get; set; }
            [Display(Name = "Wiek")]
            [Range(1,200)]
            public int Wiek { get; set; }
            public string Zdjecie { get; set; }

            public override string ToString()
            {
                return ID + " " + Imie + " " + Nazwisko + " " + Wiek;
            }
        }

}