using System.Net;

namespace UniversityCafeteriaSystem.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string 	Address{ get; set; }
        public List<Product> products{  get; set; } = new List<Product>();
    }
}
