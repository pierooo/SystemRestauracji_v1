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
        private string paymentName;
        private string selectedPaymentType;
        private bool saveAndCloseIsEnabled;
        private bool addInvoiceIsEnabled;
        private bool addDocumentIsEnabled;

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

        public string PaymentName
        {
            get
            {
                return this.paymentName;
            }
            set
            {
                if (value != paymentName)
                {
                    paymentName = value;
                    base.OnPropertyChanged(() => paymentName);
                }
            }
        }

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

        public bool SaveAndCloseIsEnabled
        {
            get
            {
                return this.saveAndCloseIsEnabled;
            }
            set
            {
                if (value != saveAndCloseIsEnabled)
                {
                    saveAndCloseIsEnabled = value;
                    base.OnPropertyChanged(() => saveAndCloseIsEnabled);
                }
            }
        }

        public bool AddDocumentIsEnabled
        {
            get
            {
                return this.addDocumentIsEnabled;
            }
            set
            {
                if (value != addDocumentIsEnabled)
                {
                    addDocumentIsEnabled = value;
                    base.OnPropertyChanged(() => addDocumentIsEnabled);
                }
            }
        }

        public bool AddInvoiceIsEnabled
        {
            get
            {
                return this.addInvoiceIsEnabled;
            }
            set
            {
                if (value != addInvoiceIsEnabled)
                {
                    addInvoiceIsEnabled = value;
                    base.OnPropertyChanged(() => addInvoiceIsEnabled);
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

        public string SelectedPaymentType
        {
            get
            {
                return this.selectedPaymentType;
            }
            set
            {
                if (value != selectedPaymentType)
                {
                    selectedPaymentType = value;
                    base.OnPropertyChanged(() => selectedPaymentType);
                    PaymentName = selectedPaymentType;

                    if (selectedPaymentType == "Karta kredytowa" || selectedPaymentType == "Przelew")
                    {
                        SaveAndCloseIsEnabled = false;
                    }
                    else
                    {
                        SaveAndCloseIsEnabled = true;
                    }
                    if (selectedPaymentType == "Płatnośc odroczona")
                    {
                        AddInvoiceIsEnabled = false;
                        AddDocumentIsEnabled = false;

                    }
                    else
                    {
                        AddInvoiceIsEnabled = true;
                        AddDocumentIsEnabled = true;
                    }
                }
            }
        }

        #endregion
        #region commands
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

        public ICommand SaveOrderForCloseAndCloseViewCommand
        {
            get
            {
                return new BaseCommand(Save);
            }
        }

        public ICommand AddDocumentCommand
        {
            get
            {
                return new BaseCommand(SaveAndOpenAddDocumentView);
            }
        }

        public ICommand AddInvoiceCommand
        {
            get
            {
                return new BaseCommand(SaveAndOpenAddInvoiceView);
            }
        }
        #endregion


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
            List<Orders> result = new List<Orders>();
            foreach (var orderId in OrderIds)
            {
                result.Add(Database.Orders.SingleOrDefault(x => x.Id == orderId));
            }

            if (result.Any())
            {
                // TODO: adjust workstation device links for order closing
                var employeeId = Database.Orders.First(x => x.Id == closeOrder.Order.Id).EmployeeId;
                var deviceId = Database.Devices.First(x => x.IsActive == true).Id;
                var payment = new Payments();
                payment.Name = String.Join(",", OrderIds) + " Płatność " + PaymentName;
                payment.Description = "Automatyczna - zamknięte zamówienie";
                payment.TotalAmountGross = totalPriceGross;
                payment.EmployeeId = employeeId;
                payment.DeviceId = deviceId;
                payment.IsActive = true;
                payment.PaymentStatus = StatusMapper.MapToDbStatus(Status.Done);
                payment.PaymentType = PaymentName;
                payment.LastModified = DateTime.Now;
                payment.PaymentDate = DateTime.Now;
                Database.Payments.AddObject(payment);
                Database.SaveChanges();
                foreach (var order in result)
                {
                    order.PaymentId = payment.Id;
                    order.LastModified = DateTime.Now;
                    if (SelectedPaymentType == "Płatnośc odroczona")
                    {
                        order.OrderStatus = StatusMapper.MapToDbStatus(Status.InProgress);
                    }
                    else
                    {
                        order.OrderStatus = StatusMapper.MapToDbStatus(Status.Done);
                    }
                    Database.SaveChanges();
                }
            }
            OnRequestClose();
        }

        public void SaveAndOpenAddDocumentView()
        {
            if (SelectedPaymentType == "Karta kredytowa" && !Database.WorkstationDeviceLinks.Any())
            {
                PaymentName = "PŁATNOŚC KARTĄ WYMAGA TERMINALA!";
            }
            else
            {
                Save();
                Messenger.Default.Send(new OrdersForDocument(OrderIds, PaymentName, false));
            }
        }

        public void SaveAndOpenAddInvoiceView()
        {
            if (SelectedPaymentType == "Karta kredytowa" && !Database.WorkstationDeviceLinks.Any())
            {
                PaymentName = "PŁATNOŚC KARTĄ WYMAGA TERMINALA!";
            }
            else
            {
                Save();
                // TODO: otworzyć dodatnie faktury 
            }
        }

        private void GetOrderDetails()
        {
            Messenger.Default.Send(new OrderForOrderDetailsView(orderForClose.Id, orderForClose.Name, Status.Open));
        }

        private void RefreshPrice()
        {
            decimal? totalPriceGross = 0m;
            foreach (var orderId in OrderIds)
            {
                Database = new RestaurantSystemEntities();
                totalPriceGross += Database.Orders.SingleOrDefault(x => x.Id == orderId).TotalPriceGross;
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
            if (closeOrder.Order != null && closeOrder.Action == "CloseNext")
            {
                OrderIds.Add(closeOrder.Order.Id);
                OrdersForCloseLabel += ", " + closeOrder.Order.Name;
                TotalPriceGross += closeOrder.Order.TotalPriceGross;
                Description += ",\r\nZamówienie " + closeOrder.Order.Name + ": " + closeOrder.Order.Description;
                EmployeeFullName += ",\r\nZamówienie " + closeOrder.Order.Name + ": " + closeOrder.Order.EmployeeFullName;
                RestaurantTableName += ",\r\nZamówienie " + closeOrder.Order.Name + ": " + closeOrder.Order.RestaurantTableName;
            }
        }

        private IQueryable<string> GetPaymentTypes()
        {
            return PaymentType.GetPaymentTypes().AsQueryable();
        }
    }
}
