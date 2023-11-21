using MyVaccinesWeb.Models;

namespace MyVaccinesWeb.Services.PatientTypesService
{
    public interface IPatientTypesService
    {
        Task<List<PatientsType>?> GetAllPatientTypesAsync();
        Task<PatientsType?> GetSinglePatientTypeAsync(int id);
        Task<bool> AddPatientTypeAsync(PatientsType patientType);
        Task<bool> DeletePatientTypeAsync(int id);
        Task<bool> UpdatePatientTypeAsync(int id, PatientsType patientType);
    }
}
