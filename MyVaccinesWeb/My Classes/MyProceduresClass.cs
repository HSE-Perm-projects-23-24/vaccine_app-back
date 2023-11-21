using Microsoft.Identity.Client;
using MyVaccinesWeb.Services.ProceduresService;

namespace MyVaccinesWeb.My_Classes
{
    public class MyProceduresClass
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string PatientType { get; set; }

        public string Vaccine { get; set; }

        public DateTime Date { get; set; }

        public MyProceduresClass(int id, string surname, string name, string patronymic, string patientType, string vaccine, DateTime date)
        {
            Id = id;
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            PatientType = patientType;
            Vaccine = vaccine;
            Date = date;
        }

        int GetPatientId(string surname, string name, string patronymic, string type, ProceduresContext context)
        {
            int typeId = ProceduresService.GetTypeId(type, context);
            int patientId = context.MyPatients.Where(p => p.Surname.Trim() == surname.Trim()
                                                    && p.Name.Trim() == name.Trim()
                                                    && p.Patronymic.Trim() == patronymic.Trim()
                                                    && p.TypeId == typeId)
                                                    .Select(p => p.Id).FirstOrDefault();
            return patientId;
        }

        int GetVaccineId(string name, ProceduresContext context)
        {
            return context.Vaccines.Where(v => v.Name.Trim() == name.Trim()).Select(v => v.Id).FirstOrDefault();
        }

        public void AddProcedure(int userId, ProceduresContext context)
        {
            int patientId = GetPatientId(this.Surname, this.Name, this.Patronymic, this.PatientType, context);
            int vaccineId = GetVaccineId(this.Vaccine, context);
            if (patientId == 0 || vaccineId == 0)
                return;
            context.MyProcedures.Add(new MyProcedure(patientId, vaccineId, this.Date, userId));
        }

        public void UpdateProcedure(int id, ProceduresContext context)
        {
            int patientId = GetPatientId(this.Surname, this.Name, this.Patronymic, this.PatientType, context);
            int vaccineId = GetVaccineId(this.Vaccine, context);
            if (patientId == 0 || vaccineId == 0)
                return;
            var procedureToUpdate = context.MyProcedures.FirstOrDefault(p => p.Id == id);
            if (procedureToUpdate != null)
            {
                procedureToUpdate.PatientId = patientId;
                procedureToUpdate.VaccineId = vaccineId;
                procedureToUpdate.Date = this.Date;
            }
        }
    }
}
