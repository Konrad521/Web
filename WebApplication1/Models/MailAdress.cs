using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebApplication1.Controllers;

namespace WebApplication1.Models
{
    public class Email
    {
        public string Subject { get; set; }
        public string Body { get; set; }
       
    }

    [Table("Table_4")]
    public class MailAdress
    {
        [Key]
        public int ID { get; set; }
        public string Adres { get; set; }

        public override string ToString()
        {
            return String.Format("ID: {0}, Adres: {1}", this.ID, this.Adres);
        }
    }


}