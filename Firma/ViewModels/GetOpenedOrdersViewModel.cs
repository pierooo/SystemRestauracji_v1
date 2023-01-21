using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SystemRestauracji.Models.BusinessLogic;
using SystemRestauracji.Models.EntitiesForView;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class GetOrdersViewModel : ViewModelBase<OrderForAllView>
    {
        private readonly Status[] statuses;
        public GetOrdersViewModel(Status[] statuses, string viewName) : base(viewName)
        {
            this.statuses = statuses;
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
