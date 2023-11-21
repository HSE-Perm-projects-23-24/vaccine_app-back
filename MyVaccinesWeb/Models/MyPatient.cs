using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyVaccinesWeb.Models;

public partial class MyPatient
{
    public int Id { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public int TypeId { get; set; }

    public MyPatient(string surname, string name, string patronymic, int typeId)
    {
        Surname = surname;
        Name = name;
        Patronymic = patronymic;
        TypeId = typeId;
    }

    [JsonIgnore]
    public virtual ICollection<MyProcedure> MyProcedures { get; set; } = new List<MyProcedure>();

    public virtual PatientsType Type { get; set; } = null!;

    public void AddPatient(ProceduresContext context)
    {
        try 
        { 
            context.MyPatients.Add(this);
            context.SaveChanges();
        }
        catch { }
    }
}
