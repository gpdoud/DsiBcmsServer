using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSI.BcmsServer.Utility {
    
    public static class Date {

        public static DateTime EasternTimeNow {
            get {
                var utcNow = DateTime.UtcNow;
                var est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                var etNow = TimeZoneInfo.ConvertTimeFromUtc(utcNow, est);
                return etNow;
            }
        }
    }
}
