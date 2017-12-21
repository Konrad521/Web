using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebApplication1.Models
{
    public class db_context : DbContext
    {

        public db_context() : base(@"Data Source=KONRAD-PC\SQLEXPRESS; database=Osoby; Integrated Security=True; user=Konrad-PC-Konrad;")
        {

        }

        public DbSet<Osoba> Osoby { get; set; }
        public DbSet<Nieruchomosc> Nieruchomosci { get; set; }
        public DbSet<Historia> HistoriaTable { get; set; }
        public DbSet<MailAdress> Adresy { get; set; }
    }
}
