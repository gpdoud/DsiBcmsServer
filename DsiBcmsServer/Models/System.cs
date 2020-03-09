using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSI.BcmsServer.Models {

    public static class System {

        public static readonly int MajorVersion = 0;
        public static readonly int MinorVersion = 5;
        public static readonly int PatchVersion = 2;
        public static readonly string StatusVersion = "BETA";
        public static readonly string Branch = "logging";
        public static string Version {
            get {
                return $"v{MajorVersion}.{MinorVersion}.{PatchVersion} [{StatusVersion}] ({Branch}) ";
            }
        }
    }
}
