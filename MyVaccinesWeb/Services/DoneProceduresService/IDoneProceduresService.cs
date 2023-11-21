using MyVaccinesWeb.My_Classes;

namespace MyVaccinesWeb.Services.DoneProceduresService
{
    public interface IDoneProceduresService
    {
        Task<List<ProceduresDone>?> GetAllDoneProceduresAsync(int userId);
        Task<bool> AddDoneProcedureAsync(string procedureId, bool fullOrNot, DateTime actualDate);
    }
}
