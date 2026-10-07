using System;

namespace CHUSHKA.Models
{
    public class Order
    {
        public int Id { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public string ClientId { get; set; } = null!;
        public ApplicationUser Client { get; set; } = null!;

        public DateTime OrderedOn { get; set; }
    }
}
