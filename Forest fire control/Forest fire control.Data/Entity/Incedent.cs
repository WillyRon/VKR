using Forest_fire_control.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forest_fire_control.Data.Models
{
    public class Incedent
    {
        public long Id { get; set; }
        public long VideoArchiveId { get; set; }
        public long ObservationSiteId { get; set; }
        public DateTime Data { get; set; }
        public IncedentStatusEnum Status { get; set; }


        public VideoArchive VideoArchive { get; set; }
        public ObservationSite ObservationSite { get; set; }
    }
}
