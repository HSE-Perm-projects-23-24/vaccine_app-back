namespace MyVaccinesWeb.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly ProceduresContext Context;

        public UserService(ProceduresContext context)
        {
            Context = context;
        }

        public bool CheckUser(User user)
        {
            return IsValid(user);
        }

        bool IsValid(User user) // существует ли такой пользователь
        {
            var users = Context.Users.ToList();
            bool isValid = true;
            foreach (var usr in users)
            {
                if (user.Username.Trim() == usr.Username.Trim() && Hashing.VerifyHashedPassword(usr.Password.Trim(), user.Password.Trim()))
                    return isValid;
            }
            return !isValid;
        }

        public async Task<List<User>?> GetUserAsync(string userName)
        {
            return await Context.Users.Where(u => u.Username == userName).ToListAsync();
        }
    }
}
