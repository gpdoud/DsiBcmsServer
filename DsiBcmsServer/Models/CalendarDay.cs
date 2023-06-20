﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DSI.BcmsServer.Models;

public class CalendarDay {

    public int Id { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public string Notes { get; set; } = string.Empty;
    public string Topic { get; set; } = string.Empty;
    public string Subtopic { get; set; } = string.Empty;
    public int WeekNbr { get; set; } = 0;
    public int DayNbr { get; set; } = 0;
    public string AssessmentToday { get; set; } = string.Empty;
    public bool GraduationToday { get; set; } = false;
    public bool NoClassToday { get; set; } = false;

    public int CalendarId { get; set; } = 0;
    [JsonIgnore]
    public virtual Calendar? Calendar { get; set; } = null;

    public bool Active { get; set; } = true;
    public DateTime? Created { get; set; } = null;
    public DateTime? Updated { get; set; } = null;
}
