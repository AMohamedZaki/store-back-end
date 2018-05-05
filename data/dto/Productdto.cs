namespace Store.data.dto { 
    public class supplierdto
    {
        public int id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public decimal Price { get; set; }

        public decimal Cost { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public ProductCategorydto productCategories { get; set; }
    }
}