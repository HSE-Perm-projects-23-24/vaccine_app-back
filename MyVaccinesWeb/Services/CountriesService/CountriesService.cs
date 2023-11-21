using Microsoft.EntityFrameworkCore;

namespace MyVaccinesWeb.Services.CountriesService
{
    public class CountriesService : ICountriesService
    {
        private readonly ProceduresContext Context;

        public CountriesService(ProceduresContext context)
        {
            Context = context;
        }

        public async Task<bool> AddCountryAsync(Country country)
        {
            if (country.Name.Trim() == "" || Regex.IsMatch(country.Name, @"\d"))
                return false;
            var countries = Context.Countries.ToList();    
            foreach (var item in countries)
            {
                if (item.Name.ToLower().Replace(" ", "") == country.Name.ToLower().Replace(" ", ""))
                    return false;
            }
            Context.Countries.Add(country);
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCountryAsync(int id)
        {
            var country = await Context.Countries.FindAsync(id);
            if (country is null)
                return false;
            Context.Countries.Remove(country);
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Country>?> GetAllCountriesAsync()
        {
            var countries = await Context.Countries.ToListAsync();
            foreach (Country item in countries)
            {
                item.Name = item.Name.Trim();
            }
            return countries;
        }

        public async Task<Country?> GetSingleCountryAsync(int id)
        {
            var country = await Context.Countries.FindAsync(id);
            country.Name = country.Name.Trim();
            return country;
        }

        public async Task<bool> UpdateCountryAsync(int id, Country country)
        {
            if (country.Name.Trim() == "" || Regex.IsMatch(country.Name, @"\d"))
                return false;
            var countries = Context.Countries.ToList();
            foreach (var item in countries)
            {
                if (item.Name.ToLower().Replace(" ", "") == country.Name.ToLower().Replace(" ", ""))
                    return false;
            }
            var countryToUpdate = Context.Countries.FirstOrDefault(c => c.Id == id);
            if (countryToUpdate != null)
            {
                countryToUpdate.Name = country.Name;
            }
            return await Context.SaveChangesAsync() >= 1;
        }
    }
}
