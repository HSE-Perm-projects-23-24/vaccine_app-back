using MyVaccinesWeb.Models;

namespace MyVaccinesWeb.Services.DoneProceduresService
{
    public class DoneProceduresService : IDoneProceduresService
    {
        private readonly ProceduresContext Context;

        public DoneProceduresService(ProceduresContext context)
        {
            Context = context;
        }

        public async Task<bool> AddDoneProcedureAsync(string procedureId, bool fullOrNot, DateTime actualDate)
        {
            if (!Int32.TryParse(procedureId, out int id))
                return false;

            var procedures = Context.MyProcedures.ToList();
            bool isExists = false;
            foreach (var p in procedures)
            {
                if (p.Id == Convert.ToInt32(procedureId))
                {
                    isExists = true;
                    break;
                }
            }

            if (!isExists)
                return false;

            var newData = Convert.ToString(actualDate).Substring(0, 10);
            bool isDate = DateTime.TryParse(newData, out DateTime dt);

            if (!isDate)
                return false;

            Context.ProceduresDones.Add(new ProceduresDone(Convert.ToInt32(procedureId), fullOrNot, actualDate));

            return await Context.SaveChangesAsync() >= 1;
        }

        public async Task<List<ProceduresDone>?> GetAllDoneProceduresAsync(int userId)
        {
            var doneProcedures = await Context.ProceduresDones.Include(pd => pd.Procedure)
                                                .Include(pd => pd.Procedure.Patient)
                                                .Include(pd => pd.Procedure.Patient.Type)
                                                .Include(pd => pd.Procedure.Vaccine)
                                                .Include(pd => pd.Procedure.Vaccine.Type)
                                                .Where(pd => pd.Procedure.UserId == userId)
                                                .ToListAsync();

            foreach (var dp in doneProcedures)
            {
                dp.Procedure.Patient.Surname = dp.Procedure.Patient.Surname.Trim();
                dp.Procedure.Patient.Name = dp.Procedure.Patient.Name.Trim();
                dp.Procedure.Patient.Patronymic = dp.Procedure.Patient.Patronymic.Trim();
                dp.Procedure.Patient.Type.Name = dp.Procedure.Patient.Type.Name.Trim();
                dp.Procedure.Vaccine.Name = dp.Procedure.Vaccine.Name.Trim();
                dp.Procedure.Vaccine.Type.Name = dp.Procedure.Vaccine.Type.Name.Trim();
            }

            return doneProcedures;
        }
    }
}
