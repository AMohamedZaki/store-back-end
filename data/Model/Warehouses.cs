
namespace Store.data.Model { 
    public class Warehouses
    {
        public int id { get; set; }

        public string Name { get; set; }

        public int BranchId { get; set; }

        public Branches Branches { get; set; }
    }
}