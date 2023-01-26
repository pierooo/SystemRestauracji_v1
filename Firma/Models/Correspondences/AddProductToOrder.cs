using SystemRestauracji.Models.Entities;

namespace SystemRestauracji.Models.Correspondences
{
    public class AddProductToOrder
    {
        public int OrderId { get; set; }

        public string OrderFullName { get; set; }

        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }

        public Products Product { get; set; }

        public int? Quantity { get; set; }

        public bool BackToCategories { get; set; }

        public bool Added{ get; set; }

        public AddProductToOrder(int orderId, string orderFullName)
        {
            OrderId = orderId;
            OrderFullName = orderFullName;
        }
    }
}
