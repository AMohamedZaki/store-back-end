namespace Store.dto { 
    public class Productdto
    {
        public int id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public decimal Price { get; set; }

        public decimal Cost { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
    }
}