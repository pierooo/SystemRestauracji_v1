using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemRestauracji.Models.EntitiesForView
{
    public class WorkstationDeviceLinksForAllView
    {
        public int Id { get; set; }
        public string WorkstationName { get; set; }
        public string WorkstationDescription { get; set; }
        public string DeviceName { get; set; }
        public string DeviceDescription { get; set; }
    }
}
