using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTest.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public string Persone { get; set; }
        public string Adress { get; set; }
        public int BookId { get; set; }
        public DateTime Date { get; set; }
    }
}