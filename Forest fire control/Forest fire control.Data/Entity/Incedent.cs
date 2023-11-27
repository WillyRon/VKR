using Forest_fire_control.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forest_fire_control.Data.Models
{
    public class Incedent
    {
        public Guid Id { get; set; }
        public Guid VideoArchiveId { get; set; }
        public Guid ObservationSiteId { get; set; }
        public DateTime Data { get; set; }
        public IncedentStatusEnum Status { get; set; }


        public VideoArchive VideoArchive { get; set; }
        public ObservationSite ObservationSite { get; set; }
    }
}
