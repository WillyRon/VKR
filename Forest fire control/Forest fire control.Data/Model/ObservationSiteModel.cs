using System;
using System.Collections.Generic;
using System.Text;

namespace Forest_fire_control.Data.Model
{
    public class ObservationSiteModel
    {
        public string Name { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string Address { get; set; }
        public string Region { get; set; }
        public string Url { get; set; }
        public bool IsActiveIncident { get; set; }
    }
}
