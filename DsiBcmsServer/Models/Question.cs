using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DSI.BcmsServer.Models {
    
    public enum AnswerTextType { A, B, C, D, E }

    public class Question {
        public int Id { get; set; }
        public string Category { get; set; }
        public string QuestionText { get; set; }
        public string AnswerTextA { get; set; }
        public string AnswerTextB { get; set; }
        public string AnswerTextC { get; set; }
        public string AnswerTextD { get; set; }
        public string AnswerTextE { get; set; }
        public AnswerTextType CorrectAnswerNbr { get; set; }
        public int PointValue { get; set; }
        public AnswerTextType UserAnswerNbr { get; set; }

        public int EvaluationId { get; set; }
        [JsonIgnore]
        public virtual Evaluation Evaluation { get; set; }

        public int? EnrollmentId { get; set; }
        [JsonIgnore]
        public virtual Enrollment Enrollment { get; set; }

        public bool Active { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }

        public Question() { }
    }
}
