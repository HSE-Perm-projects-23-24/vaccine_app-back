using System;
using System.Collections.Generic;

namespace MyVaccinesWeb.Models;

public partial class ProceduresDone
{
    public int Id { get; set; }

    public int ProcedureId { get; set; }

    public bool FullOrNot { get; set; }

    public DateTime ActualDate { get; set; }

    public ProceduresDone(int procedureId, bool fullOrNot, DateTime actualDate)
    {
        ProcedureId = procedureId;
        FullOrNot = fullOrNot;
        ActualDate = actualDate;
    }

    public virtual MyProcedure Procedure { get; set; } = null!;
}
