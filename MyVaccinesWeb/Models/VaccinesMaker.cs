using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyVaccinesWeb.Models;

public partial class VaccinesMaker
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CountryId { get; set; }

    public VaccinesMaker(string name, int countryId)
    {
        Name = name;
        CountryId = countryId;
    }

    public virtual Country Country { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Vaccine> Vaccines { get; set; } = new List<Vaccine>();
}
