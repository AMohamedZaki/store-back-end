namespace Store.data.Model { 
    public class InvoiceDetails
    {
        public int id { get; set; }

        public int InvoiceId { get; set; }

        public int ProductId { get; set; }

        public decimal Cost { get; set; }

        public decimal Price { get; set; }
        
        public int Qty { get; set; }

        public decimal Total { get; set; }    
    }
}