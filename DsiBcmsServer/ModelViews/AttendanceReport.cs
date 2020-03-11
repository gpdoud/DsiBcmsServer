using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSI.BcmsServer.Models;

namespace DSI.BcmsServer.ModelViews {

    public class AttendanceReport {

        public List<Attendance> Attendances { get; set; }
        public User Student { get; set; }

        public AttendanceReport() { }
    }
}
