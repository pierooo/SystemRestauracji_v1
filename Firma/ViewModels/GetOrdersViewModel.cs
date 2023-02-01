using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using SystemRestauracji.Models.BusinessLogic;
using SystemRestauracji.Models.Correspondences;
using SystemRestauracji.Models.EntitiesForView;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class GetOrdersViewModel : ViewModelBase<OrderForAllView>
    {
        private readonly Status[] statuses;

        private readonly string action;

        private OrderForAllView selectedOrder;

        public OrderForAllView SelectedOrder
        {
            get
            {
                return selectedOrder;
            }
            set
            {
                if (selectedOrder != value)
                {
                    selectedOrder = value;
                    var status = Status.Done;
                    if (statuses.Any(x => x == Status.Added || x == Status.InProgress))
                    {
                        status = Status.Open;
                    }
                    if(selectedOrder != null && string.IsNullOrEmpty(action))
                    {
                        Messenger.Default.Send(new OrderForOrderDetailsView(selectedOrder.Id, selectedOrder.Name, status));
                    }
                    if(selectedOrder != null && action == "Close")
                    {
                        Messenger.Default.Send(new CloseOrder(action, selectedOrder));
                    }
                    if (selectedOrder != null && action == "CloseNext")
                    {
                        base.OnRequestClose();
                        Messenger.Default.Send(new CloseOrder(action, selectedOrder));     
                    }
                }
            }
        }

        public GetOrdersViewModel(Status[] statuses, string viewName, string action = null) : base(viewName)
        {
            this.statuses = statuses;
            this.action = action;
        }

        public override void Load()
        {
            var listForView = new List<OrderForAllView>();
            foreach(var status in statuses)
            {
                var mappedStatus = StatusMapper.MapToDbStatus(status);
                var items = restaurantEntities.Orders
                .Where(x => x.OrderStatus == mappedStatus)
                .Select(x =>
                new OrderForAllView()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    EmployeeFullName = x.Users.FirstName + " " + x.Users.LastName,
                    RestaurantTableName = x.RestaurantTables.Name,
                    OrderDate = x.OrderDate,
                    TotalPriceGross = x.TotalPriceGross,
                    OrderStatus = x.OrderStatus,
                    PaymentName = x.Payments.Name ?? "Nie zapłacono",
                    DocumentName = x.Documents.Name ?? "Nie wystawiono"
                });
                if(items != null && items.Count() > 0)
                {
                    listForView.AddRange(items);
                }
            }
            List = new ObservableCollection<OrderForAllView>(listForView);
        }
    }
}
