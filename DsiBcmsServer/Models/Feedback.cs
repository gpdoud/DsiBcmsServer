using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSI.BcmsServer.Models {

    public class Feedback {

        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Response { get; set; }
        public bool Locked { get; set; } = false; // disallow more threads
        public int NextId { get; set; } // for threads
        public int PrevId { get; set; }

        public bool Active { get; set; } = true;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }

        public Feedback() {}
    }
}
