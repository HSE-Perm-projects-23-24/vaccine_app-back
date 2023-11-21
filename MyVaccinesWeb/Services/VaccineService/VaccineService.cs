using Microsoft.EntityFrameworkCore;
using MyVaccinesWeb.My_Classes;

namespace MyVaccinesWeb.Services.VaccineService
{
    public class VaccineService : IVaccineService
    {
        private readonly ProceduresContext Context;

        public VaccineService(ProceduresContext context)
        {
            Context = context;
        }
        
        public async Task<bool> AddVaccineAsync(MyVaccineClass vaccine)
        {
            if (vaccine.Name.Trim() == "")
                return false;

            int typeId = VaccineTypesService.VaccineTypesService.GetVaccineTypeId(vaccine.Type, Context);
            int makerId = VaccineMakersService.VaccineMakersService.GetVaccineMakerId(vaccine.Maker, Context);

            var sameVaccines = Context.Vaccines.Where(v => v.Name.Trim() == vaccine.Name.Trim() &&
                                                      v.Maker.Name.Trim() == vaccine.Maker.Trim() &&
                                                      v.Type.Name.Trim() == vaccine.Type.Trim());
            if (sameVaccines.Any())
                return false;

            Context.Vaccines.Add(new Vaccine(vaccine.Name, makerId, typeId));
            return await Context.SaveChangesAsync() >= 1;
        }

        public async Task<bool> DeleteVaccineAsync(int id)
        {
            var vaccine = await Context.Vaccines.FindAsync(id);
            if (vaccine is null)
                return false;
            Context.Vaccines.Remove(vaccine);
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Vaccine>?> GetAllVaccinesAsync()
        {
            var vaccines = await Context.Vaccines.Include(v => v.Maker).Include(v => v.Type).ToListAsync();
            foreach (var vaccine in vaccines)
            {
                vaccine.Name = vaccine.Name.Trim();
                vaccine.Type.Name = vaccine.Type.Name.Trim();
                vaccine.Maker.Name = vaccine.Maker.Name.Trim();
            }
            return vaccines;
        }

        public async Task<Vaccine?> GetSingleVaccineAsync(int id)
        {
            return await Context.Vaccines.FindAsync(id);
        }

        public async Task<bool> UpdateVaccineAsync(int id, MyVaccineClass vaccine)
        {
            if (vaccine.Name.Trim() == "")
                return false;

            int typeId = VaccineTypesService.VaccineTypesService.GetVaccineTypeId(vaccine.Type, Context);
            int makerId = VaccineMakersService.VaccineMakersService.GetVaccineMakerId(vaccine.Maker, Context);

            var sameVaccines = Context.Vaccines.Where(v => v.Name.Trim() == vaccine.Name.Trim() &&
                                                      v.Maker.Name.Trim() == vaccine.Maker.Trim() &&
                                                      v.Type.Name.Trim() == vaccine.Type.Trim());
            if (sameVaccines.Any())
                return false;

            var vaccineToUpdate = Context.Vaccines.Find(id);
            if (vaccineToUpdate != null)
            {
                vaccineToUpdate.Name = vaccine.Name.Trim();
                vaccineToUpdate.MakerId = makerId;
                vaccineToUpdate.TypeId = typeId;
            }
            return await Context.SaveChangesAsync() >= 1;
        }
    }
}
