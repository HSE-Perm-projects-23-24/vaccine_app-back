using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyVaccinesWeb.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<MyProcedure> MyProcedures { get; set; } = new List<MyProcedure>();
}
