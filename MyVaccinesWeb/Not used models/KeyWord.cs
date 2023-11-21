using System;
using System.Collections.Generic;

namespace MyVaccinesWeb.Services.AdminsService;

public partial class KeyWord
{
    public int Id { get; set; }

    public string Word { get; set; } = null!;

    public virtual ICollection<ProceduresKeyWord> ProceduresKeyWords { get; set; } = new List<ProceduresKeyWord>();
}
