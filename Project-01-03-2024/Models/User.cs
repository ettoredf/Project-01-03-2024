using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_01_03_2024.Models
{
    public class User
    {
        public int IdUtente { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Indirizzo { get; set; }
        public string Citta { get; set; }
        public string CAP { get; set; }
        public string CodiceFiscale { get; set; }
        public int TotaleViolazioni { get; set; }
        public int TotalePuntiRimossi { get; set; }
    }
}