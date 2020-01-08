using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSI.BcmsServer.Models {

    public class SystemControl {

        public string Key { get; set; }
        public string Value { get; set; }
        public string Category { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }

        public SystemControl() {}
    }
}
