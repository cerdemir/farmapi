namespace farmapi.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public bool Deleted { get; set; } = false;

        public void Delete()
        {
            Deleted = true;
        }
    }
}