using System.Collections.Generic;
using System.Linq;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.Models.EntitiesForView;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class AddWorkstationDeviceLinkViewModel : ItemViewModel<WorkstationDeviceLinks>
    {
        private List<Workstations> workstations;
        private List<Devices> devices;

        #region prop
        public int? WorkstationId
        {
            get
            {
                return this.Item.WorkstationId;
            }
            set
            {
                if (value != Item.WorkstationId)
                {
                    Item.WorkstationId = value;
                    base.OnPropertyChanged(() => DeviceId);
                }
            }
        }

        public int? DeviceId
        {
            get
            {
                return this.Item.DeviceId;
            }
            set
            {
                if (value != Item.DeviceId)
                {
                    Item.DeviceId = value;
                    base.OnPropertyChanged(() => DeviceId);
                }
            }
        }

        public IQueryable<KeyAndValue> Devices
        {
            get
            {
                return GetDevices();
            }
        }

        public IQueryable<KeyAndValue> Workstations
        {
            get
            {
                return GetWorkstations();
            }
        }
        #endregion

        public AddWorkstationDeviceLinkViewModel() : base("Nowe Połączenie")
        {
            base.Item = new WorkstationDeviceLinks();
        }

        public override void Save()
        {
            Item.IsActive = true;
            Database.WorkstationDeviceLinks.AddObject(Item);
            Database.SaveChanges();
        }

        private IQueryable<KeyAndValue> GetDevices()
        {
            this.devices = Database.Devices.ToList();
            return devices.Select(x => new KeyAndValue { Key = x.Id, Value = x.Name }).ToList().AsQueryable();
        }

        private IQueryable<KeyAndValue> GetWorkstations()
        {
            this.workstations = Database.Workstations.ToList();
            return workstations.Select(x => new KeyAndValue { Key = x.Id, Value = x.Name }).ToList().AsQueryable();
        }
    }
}
