using MyVaccinesWeb.Models;

namespace MyVaccinesWeb.Services.CountriesService
{
    public interface ICountriesService
    {
        Task<List<Country>?> GetAllCountriesAsync();
        Task<Country?> GetSingleCountryAsync(int id);
        Task<bool> AddCountryAsync(Country country);
        Task<bool> DeleteCountryAsync(int id);
        Task<bool> UpdateCountryAsync(int id, Country country);
    }
}
