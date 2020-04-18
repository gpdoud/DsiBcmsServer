using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DSI.BcmsServer.Models {
    public class Kb {

        public int Id { get; set; }
        [StringLength(255)]
        [Required]
        public string Title { get; set; }
        [MaxLength]
        [Required]
        public string Text { get; set; }
        [MaxLength]
        public string Response { get; set; }
        public bool Sticky { get; set; } = false; // always show in list
        public bool Locked { get; set; } = false; // disallow more threads
        public int NextId { get; set; } // for threads
        public int PrevId { get; set; }
        public string ImagePath1 { get; set; } = null;

        public int UserId { get; set; }
        public virtual User User { get; set; }

        // [ForeignKey("KbCategory")]
        public int? KbCategoryId { get; set; } = null;
        public virtual KbCategory KbCategory { get; set; }

        public bool Active { get; set; } = true;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }

        public Kb() { }
    }
}
