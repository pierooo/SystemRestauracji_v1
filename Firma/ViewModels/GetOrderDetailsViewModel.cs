using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using SystemRestauracji.Models.EntitiesForView;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class GetOrderDetailsViewModel : ViewModelBase<OrderDetailsForAllView>
    {
        public int OrderId { get; set; }

        public string OrderFullName { get; set; }

        public GetOrderDetailsViewModel(OrderForOrderDetailsView order, string orderId) : base("Szczegóły - " + orderId)
        {
            this.OrderId = order.Id;
            this.OrderFullName = order.Id.ToString() + "-" + order.Name;
        }

        public override void Load()
        {
            List = new ObservableCollection<OrderDetailsForAllView>(restaurantEntities.OrdersDetails
                .Where(x => x.OrderDetailsStatus != "Anulowano" && x.OrderId == OrderId)
                .Select(x =>
                new OrderDetailsForAllView()
                {
                    Id = x.Id,
                    ProductName = x.Products.Name,
                    Description = x.Description,
                    Quantity = x.Quantity,
                    UnitPriceNetto = x.UnitPriceNetto,
                    VAT = x.VAT,
                    UnitPriceGross = ((x.UnitPriceNetto + (x.UnitPriceNetto * (x.VAT / 100))) * x.Quantity)
                }));
        }
    }
}
