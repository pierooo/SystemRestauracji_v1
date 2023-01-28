using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using SystemRestauracji.Helpers;
using SystemRestauracji.Models.BusinessLogic;
using SystemRestauracji.Models.BusinessLogic.Calculations;
using SystemRestauracji.Models.Correspondences;
using SystemRestauracji.Models.EntitiesForView;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class GetOrderDetailsViewModel : ViewModelBase<OrderDetailsForAllView>
    {
        private Status orderStatus;

        public bool AddProductButtonStatus { get; set; }

        public int OrderId { get; set; }

        public string OrderFullName { get; set; }

        public string OrderName { get; set; }

        public ICommand AddProductToOrderDetailsCommand
        {
            get
            {
                if(orderStatus == Status.Open)
                {
                    return new BaseCommand(GetCategories);
                }
                return null;
            }
        }

        public GetOrderDetailsViewModel(OrderForOrderDetailsView order, string orderId) : base("Szczegóły - " + orderId)
        {
            this.OrderId = order.Id;
            this.OrderFullName = order.Id.ToString() + "-" + order.Name;
            this.OrderName = order.Name;
            this.orderStatus = order.Status;
            this.AddProductButtonStatus = order.Status == Status.Open ? true : false;
        }

        public override void Load()
        {
            if(restaurantEntities.OrdersDetails.Any(x => x.OrderId == OrderId))
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
                    UnitPriceGross = x.UnitPriceGross
            }));
            }
        }

        private void GetCategories()
        {
            Messenger.Default.Send(new AddProductToOrder(OrderId, OrderFullName, OrderName));
        }
    }
}
