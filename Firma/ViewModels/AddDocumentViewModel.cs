using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Input;
using SystemRestauracji.Helpers;
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

        private List<DocumentPositionForDocumentView> documentPositionForDocumentView;

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

        public IQueryable<KeyValuePair<decimal?, decimal>> VATs
        {
            get
            {
                return GetVATs();
            }
        }

        public IQueryable<DocumentPositionForDocumentView> DocumentPositions { get; set; }
        #endregion

        public ICommand SaveDocumentAndCloseViewCommand
        {
            get
            {
                return new BaseCommand(Save);
            }
        }

        public AddDocumentViewModel(OrdersForDocument ordersForDocument) : base("Wystaw rachunek")
        {
            base.Item = new Documents();
            this.ordersForDocument = ordersForDocument;
            this.PaymentType = ordersForDocument.PaymentType;
            this.DocumentPositions = GetDocumentPosition();
        }

        public override void Save()
        {
            SaveDocument();
            AddDocumentToOrder();
            OnRequestClose();
        }

        private void SaveDocument()
        {
            Item.Name = "Rachunek " + DateTime.Now.Day + " " + String.Join(" ", ordersForDocument.OrderIds);
            Item.Description = "Rachunek z systemu wystawiony do zamówień: " + String.Join(", ", ordersForDocument.OrderIds);
            Item.EmployeeFullName = Employee;
            Item.DocumentDate = DocumentDate;
            Item.TotalAmountNet = AmountNet;
            Item.TotalAmountVAT = TotalVAT;
            Item.TotalAmountGross = AmountGross;
            Item.DocumentStatus = StatusMapper.MapToDbStatus(Status.Done);
            Item.Printed = false;
            Item.IsActive = true;
            Item.LastModified = DateTime.Now;
            Database.Documents.AddObject(Item);
            foreach (var documentPosition in documentPositionForDocumentView)
            {
                Item.DocumentPositionsDetails.Add(MapToDbEntity(documentPosition, Item.Id));
            }

            var tmp = Database.SaveChanges();
        }

        private void AddDocumentToOrder()
        {
            foreach(var orderId in ordersForDocument.OrderIds)
            {
                var result = Database.Orders.SingleOrDefault(x => x.Id == orderId);
                result.LastModified = DateTime.Now;               
                result.DocumentId = Item.Id;
                Database.SaveChanges();
            }
        }

        private DocumentPositionsDetails MapToDbEntity(DocumentPositionForDocumentView documentPositionForDocumentView, int documentId)
        {
            return new DocumentPositionsDetails()
            {
                PositionName = documentPositionForDocumentView.ProductName,
                DucumentId = documentId,
                UnitPriceNet = documentPositionForDocumentView.UnitPriceNet,
                UnitPriceGross = documentPositionForDocumentView.UnitPriceGross,
                VAT = documentPositionForDocumentView.VAT,
                Quantity = documentPositionForDocumentView.Quantity,
                Description = "Pozycja dodana z zamówienia",
                IsActive = true,
                LastModified = DateTime.Now
            };
        }

        private IQueryable<string> GetUsers()
        {
            this.employees = Database.Users.Where(x => x.Role == (int)Role.Employee).ToList();
            return employees.Select(x => x.FirstName + " " + x.LastName).ToList().AsQueryable();
        }

        private IQueryable<DocumentPositionForDocumentView> GetDocumentPosition()
        {
            var documentDetails = new List<DocumentPositionForDocumentView>();
            foreach (var orderId in ordersForDocument.OrderIds)
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

            this.documentPositionForDocumentView = documentDetails;

            return documentDetails.AsQueryable();
        }

        private IQueryable<KeyValuePair<decimal?, decimal>> GetVATs()
        {
            return documentPositionForDocumentView.GroupBy(x => x.VAT)
                .ToDictionary(x => x.Key,
                x => x.Sum(t => Convert.ToDecimal(t.UnitPriceGross - t.UnitPriceNet))).AsQueryable();
        }
    }
}
