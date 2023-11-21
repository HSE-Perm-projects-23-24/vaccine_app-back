using MyVaccinesWeb.My_Classes;

namespace MyVaccinesWeb.Services.VaccineMakersService
{
    public interface IVaccineMakersService
    {
        Task<bool> DeleteVaccineMakerAsync(int id);
        Task<List<VaccinesMaker>?> GetAllVaccineMakersAsync();
        Task<VaccinesMaker?> GetSingleVaccineMakerAsync(int id);
        Task<bool> UpdateVaccineMakerAsync(int id, MyVaccineMakers vaccineMaker);
        Task<bool> AddVaccineMakerAsync(MyVaccineMakers vaccineMaker);
    }
}
