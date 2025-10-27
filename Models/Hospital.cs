namespace EC4clase1.Models
{
    public class Hospital
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int Type { get; set; }
    }
}
