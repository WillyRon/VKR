using Forest_fire_control.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forest_fire_control.Data.Models
{
    public class Application
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? ObservationSiteId { get; set; }
        public DateTime Data { get; set; }
        public string Description { get; set; }
        public IncedentStatusEnum Status { get; set; }

        public User User { get; set; }
        public ObservationSite ObservationSite { get; set; }
    }
}
