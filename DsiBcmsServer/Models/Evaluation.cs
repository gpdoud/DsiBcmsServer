using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSI.BcmsServer.Models {

    public class Evaluation {
        public int Id { get; set; }
        public string Description { get; set; }

        public virtual IEnumerable<Question> Questions { get; set; }

        public bool Active { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }

        public Evaluation() {}
    }
}
