using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DSI.BcmsServer.Models {

    public class Evaluation {
        public int Id { get; set; }
        [StringLength(80)]
        [Required]
        public string Description { get; set; }
        public bool IsTemplate { get; set; } = false;
        public bool IsDone { get; set; } = false;
        public int PointsAvailable { get; set; } = 0;
        public int PointsScored { get; set; } = 0;

        public virtual IEnumerable<Question> Questions { get; set; }

        public int? EnrollmentId { get; set; }
        //[JsonIgnore]
        public virtual Enrollment Enrollment { get; set; }

        public int? UserId { get; set; }
        public virtual User User { get; set; }

        public bool Active { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }

        public Evaluation(Evaluation fromEval, int? enrollId = null) {
            Id = 0;
            Description = fromEval.Description;
            EnrollmentId = enrollId;
            PointsAvailable = fromEval.PointsAvailable;
            PointsScored = fromEval.PointsScored;
            UserId = fromEval.UserId;
            Active = true;
            Created = fromEval.Created;
            Updated = fromEval.Updated;
        }
        public Evaluation() {}
    }
}
