using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DSI.BcmsServer.Models {

    public class Enrollment {
        public int UserId { get; set; }
        public int CohortId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }
        public virtual Cohort Cohort { get; set; }

        public Enrollment() { }
    }
}
