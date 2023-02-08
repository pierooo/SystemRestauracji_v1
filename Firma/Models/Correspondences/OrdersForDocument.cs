using System.Collections.Generic;

namespace SystemRestauracji.Models.Correspondences
{
    public class OrdersForDocument
    {
        public List<int> OrderIds { get; set; }

        public string PaymentType { get; set; }

        public bool IsForInvoice { get; set; }

        public OrdersForDocument(List<int> orderIds, string paymentType, bool isForInvoice)
        {
            OrderIds = orderIds;
            PaymentType = paymentType;
            IsForInvoice = isForInvoice;
        }
    }
}
