using System;
using System.Collections.Generic;
using System.Linq;
using SystemRestauracji.Models.BusinessLogic;
using SystemRestauracji.Models.Correspondences;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.Models.EntitiesForView;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class AddDocumentViewModel : ItemViewModel<Documents>
    {
        private OrdersForDocument ordersForDocument;
        private List<Users> employees;
        private string employee;
        private bool saveAndCloseIsEnabled;

        #region prop
        public decimal? AmountNet { get; set; } = 0;

        public decimal? TotalVAT { get; set; } = 0;

        public decimal? AmountGross { get; set; } = 0;

        public string PaymentType { get; set; }

        public DateTime DocumentDate { get; set; } = DateTime.Today;

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

        public string Employee
        {
            get
            {
                return this.employee;
            }
            set
            {
                if (value != employee)
                {
                    employee = value;
                    base.OnPropertyChanged(() => employee);
                    SaveAndCloseIsEnabled = true;
                }
            }
        }

        public IQueryable<string> Employees
        {
            get
            {
                return GetUsers();
            }
        }

        public IQueryable<DocumentPositionForDocumentView> DocumentPositions
        {
            get
            {
                return GetDocumentPosition();
            }
        }
        #endregion

        public AddDocumentViewModel(OrdersForDocument ordersForDocument) : base("Wystaw rachunek")
        {
            base.Item = new Documents();
            this.ordersForDocument = ordersForDocument;
            this.PaymentType = ordersForDocument.PaymentType;
        }

        public override void Save()
        {
        }

        private IQueryable<string> GetUsers()
        {
            this.employees = Database.Users.Where(x => x.Role == (int)Role.Employee).ToList();
            return employees.Select(x => x.FirstName + " " + x.LastName).ToList().AsQueryable();
        }

        private IQueryable<DocumentPositionForDocumentView> GetDocumentPosition()
        {
            var documentDetails = new List<DocumentPositionForDocumentView>();
            foreach(var orderId in ordersForDocument.OrderIds)
            {
                documentDetails.AddRange(Database.OrdersDetails.Where(x => x.OrderId == orderId)
                    .Select(x => new DocumentPositionForDocumentView()
                    {
                        ProductName = x.Products.Name,
                        UnitPriceNet = x.UnitPriceNetto,
                        UnitPriceGross = x.UnitPriceGross,
                        VAT = x.VAT,
                        Quantity = x.Quantity
                    }));
            }

            this.AmountNet = documentDetails.Sum(x => x.UnitPriceNet * x.Quantity);

            this.TotalVAT = documentDetails.Sum(x => x.UnitPriceGross - x.UnitPriceNet);

            this.AmountGross = documentDetails.Sum(x => x.UnitPriceGross);

            return documentDetails.AsQueryable();
        }
    }
}
