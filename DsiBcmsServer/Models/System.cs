using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSI.BcmsServer.Models {

    public static class System {

        public static readonly int MajorVersion = 1;
        public static readonly int MinorVersion = 4;
        public static readonly int PatchVersion = 9;
        public static readonly string StatusVersion = "PROD";
        public static readonly string Branch = "purge-log-table";
        public static string Version {
            get {
                return $"v{MajorVersion}.{MinorVersion}.{PatchVersion} [{StatusVersion}] ({Branch}) ";
            }
        }
    }
}
