using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DSI.BcmsServer.Models {

    public class Role {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsRoot { get; set; } = false;
        public bool IsAdmin { get; set; } = false;
        public bool IsStaff { get; set; } = false;
        public bool IsInstructor { get; set; } = false;
        public bool IsStudent { get; set; } = true;

        public bool Active { get; set; } = true;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<User> Users { get; set; }

        public Role() { }
    }
}
