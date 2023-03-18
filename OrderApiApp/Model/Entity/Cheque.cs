namespace OrderApiApp.Model.Entity {
    public class Cheque {
        public long Total { get; set; }
        public Client Client { get; set; }
        public DateTime Time { get; set; } = DateTime.UtcNow;
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
    }
}
