
using System.ComponentModel.DataAnnotations;

namespace Store.Model { 
    public class WarhouseStocks
    {
        public int WarehouseId { get; set; }
        public int ItemId { get; set; }
        public int OnhandQty { get; set; }
    }
}