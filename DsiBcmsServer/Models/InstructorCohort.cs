#nullable enable

using System.Text.Json.Serialization;

namespace DSI.BcmsServer.Models;

public class InstructorCohort {
    public int Id { get; set; } = 0;
    
    public int InstructorId { get; set; } = 0;
    public virtual User? Instructor{ get; set; }

    public int CohortId { get; set; } = 0;
    [JsonIgnore]
    public virtual Cohort? Cohort { get; set; }

}
