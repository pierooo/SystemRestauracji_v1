using SystemRestauracji.Models.Correspondences;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.Models.EntitiesForView;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class CloseOrderViewModel : ItemViewModel<Orders>
    {
        public OrderForAllView OrderForClose { get; set; }
        public CloseOrderViewModel(CloseOrder orderForClose) : base("Zamknij zamówienie - " + orderForClose.Order.Name)
        {
            base.Item = new Orders();
            this.OrderForClose = orderForClose.Order;
        }
        public override void Save()
        {
        }
    }
}
