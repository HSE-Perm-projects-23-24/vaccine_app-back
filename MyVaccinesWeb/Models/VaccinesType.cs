using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyVaccinesWeb.Models;

public partial class VaccinesType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Vaccine> Vaccines { get; set; } = new List<Vaccine>();
}
