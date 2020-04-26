using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DSI.BcmsServer.Models
{
    public class Commentary
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Text { get; set; }
        public bool Sensitive { get; set; } = false;
        public int? LastAcessUserId { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
        [JsonIgnore]
        public virtual User Student { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }



        public Commentary() { }
    }
}
