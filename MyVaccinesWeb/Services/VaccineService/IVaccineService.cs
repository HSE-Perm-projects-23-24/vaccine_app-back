using MyVaccinesWeb.My_Classes;

namespace MyVaccinesWeb.Services.VaccineService
{
    public interface IVaccineService
    {
        Task<List<Vaccine>?> GetAllVaccinesAsync();
        Task<Vaccine?> GetSingleVaccineAsync(int id);
        Task<bool> AddVaccineAsync(MyVaccineClass vaccine);
        Task<bool> DeleteVaccineAsync(int id);
        Task<bool> UpdateVaccineAsync(int id, MyVaccineClass vaccine);
    }
}
