using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using MyVaccinesWeb.Models;
using MyVaccinesWeb.My_Classes;
using System.Globalization;
using System.Net.WebSockets;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography.Xml;

namespace MyVaccinesWeb.Services.ProceduresService
{
    public class ProceduresService : IProceduresService
    {
        private readonly ProceduresContext Context;

        public ProceduresService(ProceduresContext context)
        {
            Context = context;
        }

        public static int GetTypeId(string type, ProceduresContext context)
        {
            return context.PatientsTypes.Where(pt => pt.Name.Trim() == type.Trim()).Select(pt => pt.Id).FirstOrDefault();
        }

        public async Task<bool> AddProcedureAsync(int userId, MyProceduresClass myProcedure)
        {
            if (myProcedure.Surname.Trim() == "" || myProcedure.Name.Trim() == ""
                || myProcedure.Patronymic.Trim() == "" || myProcedure.PatientType.Trim() == "")
                return false;
            var typeId = GetTypeId(myProcedure.PatientType, Context);
            var patient = new MyPatient(myProcedure.Surname.Trim(), myProcedure.Name.Trim(), myProcedure.Patronymic.Trim(), typeId);

            var patients = await Context.MyPatients.ToListAsync();
            bool isExists = false;
            foreach (var p in patients)
            {
                if (p.Surname.Trim() == patient.Surname &&
                    p.Name.Trim() == patient.Name &&
                    p.Patronymic.Trim() == patient.Patronymic &&
                    p.TypeId == patient.TypeId)
                    isExists = true;
            }
            if (!isExists)
                patient.AddPatient(Context);

            var newData = Convert.ToString(myProcedure.Date).Substring(0, 10);
            bool isDate = DateTime.TryParse(newData, out DateTime dt);

            if (!isDate)
                return false;

            myProcedure.AddProcedure(userId, Context);
            return await Context.SaveChangesAsync() >= 1;
        }

        public async Task<bool> DeleteProcedureAsync(int id)
        {
            var procedure = await Context.MyProcedures.FindAsync(id);
            if (procedure is null)
                return false;
            Context.MyProcedures.Remove(procedure);
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<List<MyProcedure>?> GetAllProceduresAsync(int userId)
        {
            var doneProcedures = await Context.ProceduresDones.ToListAsync();
            var procedures = await Context.MyProcedures.Where(p => p.UserId == userId)
                                                       .Include(p => p.Patient)
                                                       .Include(p => p.Patient.Type)
                                                       .Include(p => p.Vaccine)
                                                       .Include(p => p.Vaccine.Type)
                                                       .ToListAsync();
            var ids = new SortedSet<int>();
            foreach (var dp in doneProcedures)
            {
                ids.Add(dp.ProcedureId);
            }

            var result = new List<MyProcedure>();
            foreach (var p in procedures)
            {
                if (!ids.Contains(p.Id))
                {
                    p.Patient.Surname = p.Patient.Surname.Trim();
                    p.Patient.Name = p.Patient.Name.Trim();
                    p.Patient.Patronymic = p.Patient.Patronymic.Trim();
                    p.Patient.Type.Name = p.Patient.Type.Name.Trim();
                    p.Vaccine.Name = p.Vaccine.Name.Trim();
                    p.Vaccine.Type.Name = p.Vaccine.Type.Name.Trim();
                    result.Add(p);
                }
            }

            return result;
        }

        public async Task<MyProcedure?> GetSingleProcedureAsync(int id)
        {
            var procedure = await Context.MyProcedures.Include(p => p.Patient)
                                                      .Include(p => p.Patient.Type)
                                                      .Include(p => p.Vaccine)
                                                      .Include(p => p.Vaccine.Type)
                                                      .FirstOrDefaultAsync(p => p.Id == id);
            procedure.Patient.Surname = procedure.Patient.Surname.Trim();
            procedure.Patient.Name = procedure.Patient.Name.Trim();
            procedure.Patient.Patronymic = procedure.Patient.Patronymic.Trim();
            procedure.Patient.Type.Name = procedure.Patient.Type.Name.Trim();
            procedure.Vaccine.Name = procedure.Vaccine.Name.Trim();
            procedure.Vaccine.Type.Name = procedure.Vaccine.Type.Name.Trim();
            return procedure;
        }

        public async Task<bool> UpdateProcedureAsync(int id, MyProceduresClass myProcedure)
        {
            if (myProcedure.Surname.Trim() == "" || myProcedure.Name.Trim() == ""
                || myProcedure.Patronymic.Trim() == "" || myProcedure.PatientType.Trim() == "")
                return false;
            var typeId = GetTypeId(myProcedure.PatientType, Context);
            var patient = new MyPatient(myProcedure.Surname.Trim(), myProcedure.Name.Trim(), myProcedure.Patronymic.Trim(), typeId);

            var patients = await Context.MyPatients.ToListAsync();
            bool isExists = false;
            foreach (var p in patients)
            {
                if (p.Surname.Trim() == patient.Surname &&
                    p.Name.Trim() == patient.Name &&
                    p.Patronymic.Trim() == patient.Patronymic &&
                    p.TypeId == patient.TypeId)
                    isExists = true;
            }
            if (!isExists)
                patient.AddPatient(Context);

            var newData = Convert.ToString(myProcedure.Date).Substring(0, 10);
            bool isDate = DateTime.TryParse(newData, out DateTime dt);

            if (!isDate)
                return false;

            myProcedure.UpdateProcedure(id, Context);
            return await Context.SaveChangesAsync() >= 1;
        }
    }
}
