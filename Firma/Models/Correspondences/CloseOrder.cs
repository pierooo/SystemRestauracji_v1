using SystemRestauracji.Models.Entities;
using SystemRestauracji.Models.EntitiesForView;

namespace SystemRestauracji.Models.Correspondences
{
    public class CloseOrder
    {
        public string Action { get; set; }
        public OrderForAllView Order { get; set; }

        public CloseOrder(string action, OrderForAllView order)
        {
            Action = action;
            Order = order;
        }
    }
}
