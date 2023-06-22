using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DSI.BcmsServer.Models;

public class Calendar {

    public int Id { get; set; } = 0;
    public string CohortName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; } = null;
    public DateTime? EndDate { get; set; } = null;
    public DateTime? GraduationDate { get; set; } = null;
    /*
     * A template this defines a generic cohort this
     * and can be referenced when a cohort is created and
     * automatically create a this by changing the dates
     * in line with the start and end of the cohort
     */
    public bool Template { get; set; } = false;
    
    //[JsonIgnore]
    //public virtual Cohort? Cohort { get; set; } = null;

    public virtual ICollection<CalendarDay>? CalendarDays { get; set; }

    public bool Active { get; set; } = true;
    public DateTime? Created { get; set; } = null;
    public DateTime? Updated { get; set; } = null;

    public Calendar Clone() {
        var newcal = new Calendar {
            Id = 0,
            // skip cohort name
            Description = this.Description,
            StartDate = this.StartDate,
            EndDate = this.EndDate,
            GraduationDate = this.GraduationDate,
            Template = this.Template,
            Active = this.Active,
            Created = Utility.Date.EasternTimeNow,
        };
        return newcal;
    }

}
