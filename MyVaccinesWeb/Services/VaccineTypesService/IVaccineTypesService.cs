using MyVaccinesWeb.Models;

namespace MyVaccinesWeb.Services.VaccineTypesService
{
    public interface IVaccineTypesService
    {
        Task<List<VaccinesType>?> GetAllVaccineTypesAsync();
        Task<VaccinesType?> GetSingleVaccineTypeAsync(int id);
        Task<bool> AddVaccineTypeAsync(VaccinesType vaccineType);
        Task<bool> DeleteVaccineTypeAsync(int id);
        Task<bool> UpdateVaccineTypeAsync(int id, VaccinesType vaccineType);
    }
}
