using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyVaccinesWeb.Models;

public partial class MyProcedure
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public int VaccineId { get; set; }

    public int UserId { get; set; }

    public DateTime Date { get; set; }

    public MyProcedure(int patientId, int vaccineId, DateTime date, int userId)
    {
        PatientId = patientId;
        VaccineId = vaccineId;
        Date = date;
        UserId = userId;
    }

    public virtual MyPatient Patient { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<ProceduresDone> ProceduresDones { get; set; } = new List<ProceduresDone>();

    [JsonIgnore]
    public virtual ICollection<ProceduresKeyWord> ProceduresKeyWords { get; set; } = new List<ProceduresKeyWord>();

    [JsonIgnore]
    public virtual User User { get; set; } = null!;

    public virtual Vaccine Vaccine { get; set; } = null!;
}
