using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MyVaccinesWeb.Models;

namespace MyVaccinesWeb.My_Classes
{
    public class MyVaccineMakers
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public MyVaccineMakers(int id, string name, string country)
        {
            Id = id;
            Name = name;
            Country = country;
        }

        int GetCountryId(string country, ProceduresContext context)
        {
            return context.Countries.Where(c => c.Name == country).Select(c => c.Id).FirstOrDefault();
        }

        public void AddVaccineMaker(ProceduresContext context)
        {
            int countryId = GetCountryId(this.Country, context);
            if (countryId == 0)
                return;
            context.VaccinesMakers.Add(new VaccinesMaker(this.Name, countryId));
        }

        public void UpdateVaccineMaker(int id, ProceduresContext context)
        {
            int countryId = GetCountryId(this.Country, context);
            if (countryId == 0)
                return;
            var vaccineMakerToUpdate = context.VaccinesMakers.FirstOrDefault(m => m.Id == id);
            if (vaccineMakerToUpdate != null)
            {
                vaccineMakerToUpdate.Name = this.Name;
                vaccineMakerToUpdate.CountryId = countryId;
            }
        }
    }
}
