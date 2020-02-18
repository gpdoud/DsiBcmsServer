using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DSI.BcmsServer.Models {

    public class Enrollment {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CohortId { get; set; }

        //[JsonIgnore]
        public virtual User User { get; set; }
        [JsonIgnore]
        public virtual Cohort Cohort { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<Attendance> Attendances { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<Assessment> Assessments { get; set; }

        public Enrollment() { }
    }
}
