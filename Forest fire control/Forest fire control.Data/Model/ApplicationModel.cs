using Forest_fire_control.Data.Enums;
using Forest_fire_control.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forest_fire_control.Data.Model
{
    public class ApplicationModel
    {
        public string UserEmail{ get; set; }
        public ObservationSiteModel ObservationSite { get; set; }
        public DateTime Data { get; set; }
        public string Description { get; set; }
        public IncedentStatusEnum Status { get; set; }
    }
}
