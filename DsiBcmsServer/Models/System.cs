using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSI.BcmsServer.Models {

    public static class System {

        public static readonly int MajorVersion = 0;
        public static readonly int MinorVersion = 6;
        public static readonly int PatchVersion = 0;
        public static readonly string StatusVersion = "BETA";
        public static readonly string Branch = "master";
        public static string Version {
            get {
                return $"v{MajorVersion}.{MinorVersion}.{PatchVersion} [{StatusVersion}] ({Branch}) ";
            }
        }
    }
}
