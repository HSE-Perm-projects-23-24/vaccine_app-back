using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyVaccinesWeb.Models;

public partial class Vaccine
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int MakerId { get; set; }

    public int TypeId { get; set; }

    public virtual VaccinesMaker Maker { get; set; } = null!;

    public Vaccine(string name, int makerId, int typeId)
    {
        Name = name;
        MakerId = makerId;
        TypeId = typeId;
    }

    [JsonIgnore]
    public virtual ICollection<MyProcedure> MyProcedures { get; set; } = new List<MyProcedure>();

    public virtual VaccinesType Type { get; set; } = null!;
}
