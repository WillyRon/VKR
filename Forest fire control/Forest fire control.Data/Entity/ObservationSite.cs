using Forest_fire_control.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forest_fire_control.Data.Models
{
    public class ObservationSite
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string Address { get; set; }
        public string Url { get; set; }
        public Guid RegionId { get; set; }
        public bool IsActiveIncident { get; set; }

        public Region Region { get; set; }
    }
}
