using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSI.BcmsServer.Utility {
    public class Exceptions {

        public static IEnumerable<string> Flatten(Exception exception) {
            var messages = new List<string>();
            Exception ex = exception;
            while(ex != null) {
                messages.Add(ex.Message);
                ex = ex.InnerException;
            }
            return messages.ToArray();
        }
    }
}
