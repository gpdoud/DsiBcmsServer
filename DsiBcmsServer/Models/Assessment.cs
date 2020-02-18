using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSI.BcmsServer.Models {

    public class Assessment {

        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public int? PointsScore { get; set; }
        public int? PointsMax { get; set; }

        public bool Active { get; set; } = true;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }

        public int EnrollmentId { get; set; }
        public virtual Enrollment Enrollment { get; set; }

        public Assessment() { }
    }
}
