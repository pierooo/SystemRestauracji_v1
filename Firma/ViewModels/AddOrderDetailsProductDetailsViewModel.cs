using System;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using SystemRestauracji.Helpers;
using SystemRestauracji.Models.BusinessLogic;
using SystemRestauracji.Models.BusinessLogic.Calculations;
using SystemRestauracji.Models.Correspondences;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class AddOrderDetailsProductDetailsViewModel : ItemViewModel<OrdersDetails>
    {
        private AddProductToOrder addProductToOrder;

        #region prop
        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public string OrderFullName { get; set; }

        public string CategoryName { get; set; }

        public decimal? UnitPriceGross { get; set; }

        public string ImgUrl { get; set; }


        public int? Quantity
        {
            get => Item.Quantity;
            set
            {
                if (value != Item.Quantity)
                {
                    Item.Quantity = value;
                    this.OnPropertyChanged(() => Quantity);
                }
            }
        }

        public string OrderDetailDescription
        {
            get => Item.Description;
            set
            {
                if (value != Item.Description)
                {
                    Item.Description = value;
                    this.OnPropertyChanged(() => OrderDetailDescription);
                }
            }
        }
        #endregion

        private BaseCommand addToOrderDetailsCommand;
        public ICommand AddToOrderDetailsCommand
        {
            get
            {
                if (addToOrderDetailsCommand is null)
                {
                    addToOrderDetailsCommand = new BaseCommand(() => SaveAndMoveToOrder());
                    this.OnPropertyChanged(() => Image);
                }
                return addToOrderDetailsCommand;
            }
        }


        public AddOrderDetailsProductDetailsViewModel(AddProductToOrder addProductToOrder) : base("Dodaj do " + addProductToOrder.OrderFullName)
        {
            base.Item = new OrdersDetails();
            this.addProductToOrder = addProductToOrder;
            ProductName = addProductToOrder.Product.Name;
            ProductDescription = addProductToOrder.Product.Description;
            OrderFullName = addProductToOrder.OrderFullName;
            CategoryName = addProductToOrder.CategoryName;
            UnitPriceGross = addProductToOrder.Product.UnitPriceGross;
            Item.UnitPriceNetto = addProductToOrder.Product.UnitPriceNet;
            Item.ProductId = addProductToOrder.Product.Id;
            Item.VAT = addProductToOrder.Product.VAT;
            Item.OrderId = addProductToOrder.OrderId;
            Item.Quantity = Quantity = 1;
            ImgUrl = addProductToOrder.Product.PhotoUrl;
        }

        public override void Save()
        {
            SaveOrderDetails();
            UpdateOrder();
        }

        private void UpdateOrder()
        {
            var result = Database.Orders.SingleOrDefault(x => x.Id == addProductToOrder.OrderId);
            if (result != null)
            {
                result.LastModified = DateTime.Now;
                result.TotalPriceNet += Item.Quantity * Item.UnitPriceNetto;
                result.TotalPriceGross += Calculate.CalculateGrossPriceWithQuantity(Item.UnitPriceNetto, Item.VAT, Item.Quantity);
                Database.SaveChanges();
            }
        }

        private void SaveOrderDetails()
        {
            Item.OrderDetailsStatus = StatusMapper.MapToDbStatus(Status.Added);
            Item.IsActive = true;
            Item.LastModified = DateTime.Now;
            Database.OrdersDetails.AddObject(Item);
            Database.SaveChanges();
        }

        private void SaveAndMoveToOrder()
        {
            Save();
            addProductToOrder.Added = true;
            MoveToOrderDetails();
            base.OnRequestClose();
        }

        private void MoveToOrderDetails()
        {
            Messenger.Default.Send(addProductToOrder);
        }
    }
}
