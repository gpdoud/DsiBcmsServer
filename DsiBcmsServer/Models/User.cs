using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DSI.BcmsServer.Models {

    public class User {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public string WorkPhone { get; set; }
        public string RoleCode { get; set; }
        public string SecurityKey { get; set; }
        public string PinCode { get; set; }

        public bool Active { get; set; } = true;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }

        public virtual Role Role { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<Enrollment> Enrollments { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<Cohort> Cohorts { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<Feedback> Feedbacks { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<Commentary> StudentCommentaries { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<Commentary> LastAccessUserCommentaries { get; set; }

        public User() { }
    }
}
