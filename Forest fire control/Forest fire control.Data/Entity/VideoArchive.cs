using System;
using System.Collections.Generic;
using System.Text;

namespace Forest_fire_control.Data.Models
{
    public class VideoArchive
    {
        public Guid Id { get; set; }
        public Guid ObservationSiteId { get; set; }
        public Guid? IncedentId { get; set; }
        public DateTime Data { get; set; }

        public Incedent Incedent { get; set; }
        public ObservationSite ObservationSite { get; set; }
    }
}
