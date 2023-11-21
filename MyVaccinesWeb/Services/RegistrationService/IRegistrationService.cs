namespace MyVaccinesWeb.Services.RegistrationService
{
    public interface IRegistrationService
    {
        Task<bool> AddUserAsync(User user);
    }
}
