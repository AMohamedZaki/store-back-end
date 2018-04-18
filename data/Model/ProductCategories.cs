
using System.Collections.Generic;

namespace Store.data.Model { 
    public class ProductCategories
    {
        public int id { get; set; }
        public string Name { get; set; }

        public ICollection<Products> Products { get; set; }
    }
}