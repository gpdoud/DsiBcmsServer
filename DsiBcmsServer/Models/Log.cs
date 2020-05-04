using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSI.BcmsServer.Models {

    public class Log {

        public int Id { get; set; }
        public DateTime Timestamp { get; set; } = Utility.Date.EasternTimeNow;
        public string Message { get; set; }
        public LogSeverity Severity { get; set; } = LogSeverity.Info;

        public Log() { }
    }
    public enum LogSeverity { Info, Warn, Error, Fatal, Trace, Debug };
}
