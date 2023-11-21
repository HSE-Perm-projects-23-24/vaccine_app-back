using Microsoft.EntityFrameworkCore;
using MyVaccinesWeb.Models;

namespace MyVaccinesWeb.Services.VaccineTypesService
{
    public class VaccineTypesService : IVaccineTypesService
    {
        private readonly ProceduresContext Context;

        public VaccineTypesService(ProceduresContext context)
        {
            Context = context;
        }

        public async Task<bool> AddVaccineTypeAsync(VaccinesType vaccineType)
        {
            if (vaccineType.Name.Trim() == "" || Regex.IsMatch(vaccineType.Name, @"\d"))
                return false;
            var vaccineTypes = Context.VaccinesTypes.ToList();
            foreach (var item in vaccineTypes)
            {
                if (item.Name.ToLower().Trim() == vaccineType.Name.ToLower().Trim())
                    return false;
            }
            Context.VaccinesTypes.Add(vaccineType);
            return await Context.SaveChangesAsync() >= 1;
        }

        public async Task<bool> DeleteVaccineTypeAsync(int id)
        {
            var vaccineType = await Context.VaccinesTypes.FindAsync(id);
            if (vaccineType is null)
                return false;
            Context.VaccinesTypes.Remove(vaccineType);
            return await Context.SaveChangesAsync() >= 1;
        }

        public async Task<List<VaccinesType>?> GetAllVaccineTypesAsync()
        {
            return await Context.VaccinesTypes.ToListAsync();
        }

        public async Task<VaccinesType?> GetSingleVaccineTypeAsync(int id)
        {
            return await Context.VaccinesTypes.FindAsync(id);
        }

        public async Task<bool> UpdateVaccineTypeAsync(int id, VaccinesType vaccineType)
        {
            if (vaccineType.Name.Trim() == "" || Regex.IsMatch(vaccineType.Name, @"\d"))
                return false;
            var vaccineTypes = Context.VaccinesTypes.Where(vt => vt.Name == vaccineType.Name).ToList();
            if (vaccineTypes.Any())
                return false;
            var vt = Context.VaccinesTypes.Find(id);
            vt.Name = vaccineType.Name.Trim();
            return await Context.SaveChangesAsync() >= 1;
        }

        public static int GetVaccineTypeId(string name, ProceduresContext context)
        {
            var vaccineTypes = context.VaccinesTypes.ToList();
            foreach (var vt in vaccineTypes)
            {
                if (vt.Name.Trim() == name.Trim())
                    return vt.Id;
            }
            return -1;
        }
    }
}
