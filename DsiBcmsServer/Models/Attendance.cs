using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DSI.BcmsServer.Models {
    public class Attendance {
        public int Id { get; set; }
        public DateTime In { get; set; } = DateTime.Now; // at cohort
        public DateTime? Out { get; set; }
        public bool? Excused { get; set; }
        public bool? Absent { get; set; }
        public string Note { get; set; }
        public string SecureNote { get; set; }
        public int EnrollmentId { get; set; }

        public virtual Enrollment Enrollment { get; set; }

        public Attendance() {}
    }
}
