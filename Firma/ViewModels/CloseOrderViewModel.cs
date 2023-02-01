using System;
using System.Collections.Generic;
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

        private List<int> OrderIds { get; set; }
        public CloseOrder closeOrder { get; set; }
        private decimal? totalPriceGross;
        private string restaurantTableName;
        private string employeeFullName;
        private string description;
        private string ordersForCloseLabel;

        #region prop
        public string OrdersForCloseLabel
        {
            get
            {
                return this.ordersForCloseLabel;
            }
            set
            {
                if (value != ordersForCloseLabel)
                {
                    ordersForCloseLabel = value;
                    base.OnPropertyChanged(() => ordersForCloseLabel);
                }
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                if (value != description)
                {
                    description = value;
                    base.OnPropertyChanged(() => description);
                }
            }
        }

        public string EmployeeFullName
        {
            get
            {
                return this.employeeFullName;
            }
            set
            {
                if (value != employeeFullName)
                {
                    employeeFullName = value;
                    base.OnPropertyChanged(() => employeeFullName);
                }
            }
        }

        public string RestaurantTableName
        {
            get
            {
                return this.restaurantTableName;
            }
            set
            {
                if (value != restaurantTableName)
                {
                    restaurantTableName = value;
                    base.OnPropertyChanged(() => restaurantTableName);
                }
            }
        }

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

        public IQueryable<string> PaymentTypes
        {
            get
            {
                return GetPaymentTypes();
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

        public ICommand CancelOrderCommand
        {
            get
            {
                return new BaseCommand(CancelOrder);
            }
        }

        public ICommand AddNextOpenOrderCommand
        {
            get
            {
                return new BaseCommand(AddNextOpenOrder);
            }
        }

        public CloseOrderViewModel(CloseOrder orderForClose) : base("Zamknij zamówienie - " + orderForClose.Order.Name)
        {
            OrderIds = new List<int>();
            OrderIds.Add(orderForClose.Order.Id);
            base.Item = new Orders();
            Item.Id = orderForClose.Order.Id;
            this.orderForClose = orderForClose.Order;
            this.closeOrder = orderForClose;
            OrdersForCloseLabel = "Zamknij zamówienie: " + orderForClose.Order.Name;
            Description = "Zamówienie " + orderForClose.Order.Name + ": " + orderForClose.Order.Description;
            EmployeeFullName = "Zamówienie " + orderForClose.Order.Name + ": " + orderForClose.Order.EmployeeFullName;
            RestaurantTableName = "Zamówienie " + orderForClose.Order.Name + ": " + orderForClose.Order.RestaurantTableName;
            PaymentName = string.IsNullOrEmpty(orderForClose.Order.PaymentName) ? "Odroczona" : orderForClose.Order.PaymentName;
            TotalPriceGross = orderForClose.Order.TotalPriceGross;
            Messenger.Default.Register<CloseOrder>(this, AddNextOrderForClose);
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
            decimal? totalPriceGross = 0m;
            foreach(var orderId in OrderIds)
            {
                Database = new RestaurantSystemEntities();
                totalPriceGross +=  Database.Orders.SingleOrDefault(x => x.Id == orderId).TotalPriceGross;
            }
            this.TotalPriceGross = totalPriceGross;
        }

        private void CancelOrder()
        {
            List<Orders> result = new List<Orders>();
            foreach (var orderId in OrderIds)
            {
                result.Add(Database.Orders.SingleOrDefault(x => x.Id == orderId));
            }

            if (result.Any())
            {
                foreach (var order in result)
                {
                    order.LastModified = DateTime.Now;
                    order.OrderStatus = StatusMapper.MapToDbStatus(Status.Cancelled);
                    Database.SaveChanges();
                }
            }
            OnRequestClose();
        }

        private void AddNextOpenOrder()
        {
            Messenger.Default.Send(new CloseOrder("CloseNext", null));
        }
        
        private void AddNextOrderForClose(CloseOrder closeOrder)
        {
            if(closeOrder.Order != null && closeOrder.Action == "CloseNext")
            {
                OrderIds.Add(closeOrder.Order.Id);
                OrdersForCloseLabel += ", " + closeOrder.Order.Name;
                TotalPriceGross += closeOrder.Order.TotalPriceGross;
                Description += ",\r\nZamówienie " + closeOrder.Order.Name + ": " + closeOrder.Order.Description;
                EmployeeFullName += ",\r\nZamówienie " + closeOrder.Order.Name  + ": " + closeOrder.Order.EmployeeFullName;
                RestaurantTableName += ",\r\nZamówienie " + closeOrder.Order.Name + ": " + closeOrder.Order.RestaurantTableName;
            }
        }

        private IQueryable<string> GetPaymentTypes()
        {
            return PaymentType.GetPaymentTypes().AsQueryable();
        }
    }
}
