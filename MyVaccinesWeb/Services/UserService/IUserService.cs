namespace MyVaccinesWeb.Services.UserService
{
    public interface IUserService
    {
        bool CheckUser(User user);
        Task<List<User>?> GetUserAsync(string userName);
    }
}
