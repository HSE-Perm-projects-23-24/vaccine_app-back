using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyVaccinesWeb.Models;

public partial class PatientsType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<MyPatient> MyPatients { get; set; } = new List<MyPatient>();
}
