
using System.ComponentModel.DataAnnotations;

namespace Store.dto { 
    public class WarhouseStocksdto
    {
        public int WarehouseId { get; set; }
        public int ItemId { get; set; }
        public int OnhandQty { get; set; }
    }
}