

using System;
using System.Numerics;

namespace Store.data.Model { 
    public class SalesInvoices
    {
        public int id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customers { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public decimal Paid { get; set; }
        public decimal Remain { get; set; }
    }
}