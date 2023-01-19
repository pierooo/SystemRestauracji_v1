using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemRestauracji.Models.EntitiesForView
{
    public class WorkstationForAllView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string WorkstationTypeName { get; set; }
        public string RestaurantDescription { get; set; }
    }
}
