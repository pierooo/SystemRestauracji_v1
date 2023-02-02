using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SystemRestauracji.Models.BusinessLogic;
using SystemRestauracji.Models.EntitiesForView;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class GetPaymentsViewModel : ViewModelBase<PaymentsForAllView>
    {
        private readonly Status[] statuses;

        private PaymentsForAllView selectedPayment;
        public PaymentsForAllView SelectedPayment
        {
            get
            {
                return selectedPayment;
            }
            set
            {
                if (selectedPayment != value)
                {
                    selectedPayment = value;
                    var status = Status.Fee;
                    if (statuses.Any(x => x == Status.Cancelled || x == Status.Paid))
                    {
                        status = Status.Done;
                    }

                    if (statuses.Any(x => x == Status.InProgress || x == Status.Added))
                    {
                        status = Status.Open;
                    }
                }
            }
        }
        public GetPaymentsViewModel(Status[] statuses, string viewName) : base(viewName)
        {
            this.statuses = statuses;
        }

        public override void Load()
        {
            var listForView = new List<PaymentsForAllView>();
            foreach (var status in statuses)
            {
                var mappedStatus = StatusMapper.MapToDbStatus(status);
                var items = restaurantEntities.Payments
                .Where(x => x.PaymentStatus == mappedStatus)
                .Select(x =>
                new PaymentsForAllView()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    EmployeeFullName = x.Users.FirstName + " " + x.Users.LastName,
                    DeviceName = x.Devices.Name ?? "Brak urządzenia",
                    TotalAmountGross = x.TotalAmountGross,
                    PaymentStatus = x.PaymentStatus,
                    PaymentType = x.PaymentType,
                    PaymentDate = x.PaymentDate,
                    PaymentDateLimit = x.PaymentDateLimit,
                    LastModified = x.LastModified
                });
                if (items != null && items.Count() > 0)
                {
                    listForView.AddRange(items);
                }
            }
            List = new ObservableCollection<PaymentsForAllView>(listForView);
        }
    }
}
