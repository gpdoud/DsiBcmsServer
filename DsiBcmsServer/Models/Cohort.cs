using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DSI.BcmsServer.Models {

    public class Cohort {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? InstructorId { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? DemoDay { get; set; }
        public int Capacity { get; set; }

        public bool Active { get; set; } = true;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
        
        //[JsonIgnore]
        public virtual IEnumerable<Enrollment> Enrollments { get; set; }
        public virtual User Instructor { get; set; }

        public Cohort() { }
    }
}
