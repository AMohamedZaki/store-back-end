
using System;
using System.Numerics;

namespace Store.dto { 
    public class PurchaseInvoicdto
    {
        public int id { get; set; }

        public int SupplierId { get; set; }

        public int Number { get; set; }

        public DateTime Date { get; set; }

        public decimal Total { get; set; }

        public decimal Paid { get; set; }

        public decimal Remain { get; set; }
    }
}