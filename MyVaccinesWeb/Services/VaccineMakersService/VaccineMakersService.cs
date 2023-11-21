using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MyVaccinesWeb.Models;
using MyVaccinesWeb.My_Classes;
using System.Diagnostics.Metrics;
using System.Net.WebSockets;

namespace MyVaccinesWeb.Services.VaccineMakersService
{
    public class VaccineMakersService : IVaccineMakersService
    {
        private readonly ProceduresContext Context;

        public VaccineMakersService(ProceduresContext context)
        {
            Context = context;
        }

        public async Task<bool> AddVaccineMakerAsync(MyVaccineMakers vaccineMaker)
        {
            if (vaccineMaker.Name.Trim() == "")
                return false;
            vaccineMaker.AddVaccineMaker(Context);
            return await Context.SaveChangesAsync() >= 1;
        }

        public async Task<bool> DeleteVaccineMakerAsync(int id)
        {
            var vaccineMaker = await Context.VaccinesMakers.FindAsync(id);
            if (vaccineMaker is null)
                return false;
            Context.VaccinesMakers.Remove(vaccineMaker);
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<List<VaccinesMaker>?> GetAllVaccineMakersAsync()
        {
            var vaccineMakers = await Context.VaccinesMakers.Include(vm => vm.Country).ToListAsync();
            foreach (var vm in vaccineMakers)
            {
                vm.Name = vm.Name.Trim();
                vm.Country.Name = vm.Country.Name.Trim();
            }
            return vaccineMakers;
        }

        public async Task<VaccinesMaker?> GetSingleVaccineMakerAsync(int id)
        {
            var vaccineMaker = await Context.VaccinesMakers.Include(vm => vm.Country).FirstOrDefaultAsync(vm => vm.Id == id);
            vaccineMaker.Name = vaccineMaker.Name.Trim();
            vaccineMaker.Country.Name = vaccineMaker.Country.Name.Trim();
            return vaccineMaker;
        }

        public async Task<bool> UpdateVaccineMakerAsync(int id, MyVaccineMakers vaccineMaker)
        {
            if (vaccineMaker.Name.Trim() == "")
                return false;
            vaccineMaker.UpdateVaccineMaker(id, Context);
            return await Context.SaveChangesAsync() >= 1;
        }

        public static int GetVaccineMakerId(string name, ProceduresContext context)
        {
            var vaccineMakers = context.VaccinesMakers.ToList();
            foreach (var vm in vaccineMakers)
            {
                if (vm.Name.Trim() == name.Trim())
                    return vm.Id;
            }
            return -1;
        }
    }
}
