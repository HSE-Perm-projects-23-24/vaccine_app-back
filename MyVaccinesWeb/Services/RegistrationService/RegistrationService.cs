namespace MyVaccinesWeb.Services.RegistrationService
{
    public class RegistrationService : IRegistrationService
    {
        private readonly ProceduresContext Context;

        public RegistrationService(ProceduresContext context)
        {
            Context = context;
        }

        public async Task<bool> AddUserAsync(User user)
        {
            if (!IsExist(user) && user.Username.Trim() != "" && user.Password.Trim() != "")
            {
                user.Username = user.Username.Trim();
                user.Password = Hashing.HashPassword(user.Password);
                Context.Users.Add(user);
            }
            return await Context.SaveChangesAsync() >= 1;
        }

        bool IsExist(User user) // существует ли такой пользователь
        {
            var users = Context.Users.ToList();
            bool isExist = true;
            foreach (var usr in users)
            {
                if (user.Username.Trim() == usr.Username.Trim() && Hashing.VerifyHashedPassword(usr.Password.Trim(), user.Password.Trim()))
                    return isExist;
            }
            return !isExist;
        }
    }
}
