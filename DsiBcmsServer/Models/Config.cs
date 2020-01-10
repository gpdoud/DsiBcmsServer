using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSI.BcmsServer.Models {

    public class Config {

        public string KeyValue { get; set; }
        public string DataValue { get; set; }
        public bool System { get; set; } = false;
        public bool Active { get; set; } = true;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }

        public Config() {}
    }
}
