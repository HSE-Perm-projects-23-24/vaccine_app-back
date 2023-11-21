using Microsoft.EntityFrameworkCore;
using MyVaccinesWeb.Models;

namespace MyVaccinesWeb.Services.PatientTypesService
{
    public class PatientTypesService : IPatientTypesService
    {
        private readonly ProceduresContext Context;

        public PatientTypesService(ProceduresContext context)
        {
            Context = context;
        }

        public async Task<bool> AddPatientTypeAsync(PatientsType patientType)
        {
            if (patientType.Name.Trim() == "" || Regex.IsMatch(patientType.Name, @"\d"))
                return false;
            var patientTypes = Context.PatientsTypes.ToList();
            foreach (var pt in patientTypes)
            {
                if (pt.Name.ToLower().Trim() == patientType.Name.ToLower().Trim())
                    return false;
            }
            Context.PatientsTypes.Add(patientType);
            return await Context.SaveChangesAsync() >= 1;
        }

        public async Task<bool> DeletePatientTypeAsync(int id)
        {
            var patientType = await Context.PatientsTypes.FindAsync(id);
            if (patientType is null)
                return false;
            Context.PatientsTypes.Remove(patientType);
            return await Context.SaveChangesAsync() >= 1;
        }

        public async Task<List<PatientsType>?> GetAllPatientTypesAsync()
        {
            var patientsTypes = await Context.PatientsTypes.ToListAsync();
            foreach (var pt in patientsTypes)
            {
                pt.Name = pt.Name.Trim();
            }
            return patientsTypes;
        }

        public async Task<PatientsType?> GetSinglePatientTypeAsync(int id)
        {
            return await Context.PatientsTypes.FindAsync(id);
        }

        public async Task<bool> UpdatePatientTypeAsync(int id, PatientsType patientType)
        {
            if (patientType.Name.Trim() == "" || Regex.IsMatch(patientType.Name, @"\d"))
                return false;
            var patientTypes = Context.PatientsTypes.Where(pt => pt.Name == patientType.Name).ToList();
            if (patientTypes.Any())
                return false;
            var pt = Context.PatientsTypes.Find(id);
            pt.Name = patientType.Name.Trim();
            return await Context.SaveChangesAsync() >= 1;
        }
    }
}
