using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{

    [Table("Table_3")]
    public class Historia
    {
        [Key]
       public int ID { get; set; }
       public DateTime Date { get; set; }
       public string Action { get; set; }

        
    }
}