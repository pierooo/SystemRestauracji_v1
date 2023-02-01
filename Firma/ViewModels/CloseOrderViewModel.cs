using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using SystemRestauracji.Helpers;
using SystemRestauracji.Models.BusinessLogic;
using SystemRestauracji.Models.Correspondences;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.Models.EntitiesForView;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class CloseOrderViewModel : ItemViewModel<Orders>
    {
        private OrderForAllView orderForClose;
        private decimal? totalPriceGross;

        #region prop
        public string OrdersForCloseLabel { get; set; }

        public string Desctiption { get; set; }

        public string EmployeeFullName { get; set; }

        public string RestaurantTableName { get; set; }

        public string PaymentName { get; set; }

        public decimal? TotalPriceGross
        {
            get
            {
                return this.totalPriceGross;
            }
            set
            {
                if (value != totalPriceGross)
                {
                    totalPriceGross = value;
                    base.OnPropertyChanged(() => totalPriceGross);
                }
            }
        }
        #endregion

        public ICommand GetOrderDetailsCommand
        {
            get
            {            
                return new BaseCommand(GetOrderDetails);
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new BaseCommand(RefreshPrice);
            }
        }

        public CloseOrderViewModel(CloseOrder orderForClose) : base("Zamknij zamówienie - " + orderForClose.Order.Name)
        {
            base.Item = new Orders();
            Item.Id = orderForClose.Order.Id;
            this.orderForClose = orderForClose.Order;
            OrdersForCloseLabel = "Zamknij zamówienie: " + orderForClose.Order.Name;
            Desctiption = orderForClose.Order.Description;
            EmployeeFullName = orderForClose.Order.EmployeeFullName;
            RestaurantTableName = orderForClose.Order.RestaurantTableName;
            PaymentName = string.IsNullOrEmpty(orderForClose.Order.PaymentName) ? "Odroczona" : orderForClose.Order.PaymentName;
            TotalPriceGross = orderForClose.Order.TotalPriceGross;
        }

        public override void Save()
        {
        }

        private void GetOrderDetails()
        {
            Messenger.Default.Send(new OrderForOrderDetailsView(orderForClose.Id, orderForClose.Name, Status.Open));
        }

        private void RefreshPrice()
        {
            this.TotalPriceGross = Database.Orders.SingleOrDefault(x => x.Id == orderForClose.Id).TotalPriceGross;
        }
    }
}
