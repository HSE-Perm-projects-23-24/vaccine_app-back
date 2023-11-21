using System;
using System.Collections.Generic;
using MyVaccinesWeb.Services.AdminsService;

namespace MyVaccinesWeb.Models;

public partial class ProceduresKeyWord
{
    public int Id { get; set; }

    public int KeyWordId { get; set; }

    public int ProcedureId { get; set; }

    public virtual KeyWord KeyWord { get; set; } = null!;

    public virtual MyProcedure Procedure { get; set; } = null!;
}
