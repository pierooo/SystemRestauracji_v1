using System;
using System.Collections.Generic;
using System.Linq;
using SystemRestauracji.Models.BusinessLogic.Calculations;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.Models.EntitiesForView;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class AddProductViewModel : ItemViewModel<Products>
    {
        private List<Categories> categories;

        #region prop
        public List<decimal?> VATs { get; set; }

        public string Name
        {
            get => Item.Name;
            set
            {
                if (value != Item.Name)
                {
                    Item.Name = value;
                    this.OnPropertyChanged(() => Name);
                }
            }
        }

        public string Description
        {
            get => Item.Description;
            set
            {
                if (value != Item.Description)
                {
                    Item.Description = value;
                    this.OnPropertyChanged(() => Description);
                }
            }
        }

        public int? CategoryId
        {
            get
            {
                return this.Item.CategoryId;
            }
            set
            {
                if (value != Item.CategoryId)
                {
                    Item.CategoryId = value;
                    base.OnPropertyChanged(() => CategoryId);
                }
            }
        }

        public decimal? UnitPriceGross
        {
            get => Item.UnitPriceGross;
            set
            {
                if (value != Item.UnitPriceGross)
                {
                    Item.UnitPriceGross = value;
                    this.OnPropertyChanged(() => UnitPriceGross);
                }
            }
        }

        public decimal? VAT
        {
            get => Item.VAT;
            set
            {
                if (value != Item.VAT)
                {
                    Item.VAT = value;
                    this.OnPropertyChanged(() => VAT);
                }
            }
        }

        public int? UnitsInStock
        {
            get => Item.UnitsInStock;
            set
            {
                if (value != Item.UnitsInStock)
                {
                    Item.UnitsInStock = value;
                    this.OnPropertyChanged(() => UnitsInStock);
                }
            }
        }

        public IQueryable<KeyAndValue> Categories
        {
            get
            {
                return GetCategories();
            }
        }
        #endregion

        public AddProductViewModel() : base("Nowy produkt")
        {
            base.Item = new Products();
            VATs = GetVATs().ToList();
        }

        public override void Save()
        {
            Item.UnitPriceNet = Calculate.CalculateNetPrice(Item.UnitPriceGross, Item.VAT);
            Item.Discontinued = false;
            Item.PhotoUrl = ImageUrl ?? ImageUrl;
            Item.IsActive = true;
            Item.LastModified = DateTime.Now;
            Database.Products.AddObject(Item);
            Database.SaveChanges();
        }

        private IQueryable<KeyAndValue> GetCategories()
        {
            this.categories = Database.Categories.ToList();
            return categories.Select(x => new KeyAndValue { Key = x.Id, Value = x.Name }).ToList().AsQueryable();
        }

        private IEnumerable<decimal?> GetVATs()
        {
            for(int i = 0; i<=30; i++)
            {
                yield return (decimal?)i;
            }
        }
    }
}
