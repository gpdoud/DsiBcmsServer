using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSI.BcmsServer.Models
{
    public class Commentary
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public int LastAcessUserId { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
        public virtual User User { get; set; } 



        public Commentary() { }
    }
}
