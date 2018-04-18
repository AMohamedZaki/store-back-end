
namespace Store.data.Model { 
    public class PurchInvoiceDetails
    {
        public int id { get; set; }
        public int PurchInvoiceId { get; set; }
        public PurchaseInvoices PurchaseInvoices { get; set; }
        public int ProductId { get; set; }
        public Products Products { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public decimal Total { get; set; }
    }
}