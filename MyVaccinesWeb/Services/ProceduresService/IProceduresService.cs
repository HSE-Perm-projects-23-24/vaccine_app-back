using MyVaccinesWeb.My_Classes;

namespace MyVaccinesWeb.Services.ProceduresService
{
    public interface IProceduresService
    {
        Task<bool> DeleteProcedureAsync(int id);
        Task<List<MyProcedure>?> GetAllProceduresAsync(int userId);
        Task<MyProcedure?> GetSingleProcedureAsync(int id);
        Task<bool> UpdateProcedureAsync(int id, MyProceduresClass myProcedure);
        Task<bool> AddProcedureAsync(int userId, MyProceduresClass myProcedure);
    }
}
