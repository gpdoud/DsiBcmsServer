using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DSI.BcmsServer.Models {
    
    public enum AnswerTextType { NoAnswer = -1, A, B, C, D, E }

    public class Question {
        public int Id { get; set; }
        [StringLength(30)]
        [Required]
        public string Category { get; set; }
        [StringLength(255)]
        [Required]
        public string QuestionText { get; set; }
        [StringLength(255)]
        [Required]
        public string AnswerTextA { get; set; }
        [StringLength(255)]
        [Required]
        public string AnswerTextB { get; set; }
        [StringLength(255)]
        public string AnswerTextC { get; set; }
        [StringLength(255)]
        public string AnswerTextD { get; set; }
        [StringLength(255)]
        public string AnswerTextE { get; set; }
        public AnswerTextType CorrectAnswerNbr { get; set; } = AnswerTextType.NoAnswer;
        public int PointValue { get; set; }
        public AnswerTextType UserAnswerNbr { get; set; }

        public int EvaluationId { get; set; }
        [JsonIgnore]
        public virtual Evaluation Evaluation { get; set; }
        public bool IsBonus { get; set; }

        public bool Active { get; set; } = true;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }

        public Question(Question q, int evaluationId) {
            Id = 0;
            Category = q.Category;
            QuestionText = q.QuestionText;
            AnswerTextA = q.AnswerTextA;
            AnswerTextB = q.AnswerTextB;
            AnswerTextC = q.AnswerTextC;
            AnswerTextD = q.AnswerTextD;
            AnswerTextE = q.AnswerTextE;
            CorrectAnswerNbr = q.CorrectAnswerNbr;
            PointValue = q.PointValue;
            UserAnswerNbr = q.UserAnswerNbr;
            IsBonus = q.IsBonus;
            EvaluationId = evaluationId;
            Active = q.Active;
            Created = q.Created;
            Updated = q.Updated;
        }
        public Question() { }
    }
}
