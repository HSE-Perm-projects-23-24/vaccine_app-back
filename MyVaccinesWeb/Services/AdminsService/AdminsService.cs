namespace MyVaccinesWeb.Services.AdminsService
{
    public class AdminsService : IAdminsService
    {
        private readonly ProceduresContext Context;

        public AdminsService(ProceduresContext context)
        {
            Context = context;
        }

        public bool CheckAdmin(Admin admin)
        {
            return IsValid(admin);
        }

        bool IsValid(Admin admin) // существует ли такой пользователь
        {
            var admins = Context.Admins.ToList();
            bool isValid = true;
            foreach (var adm in admins)
            {
                if (admin.Username.Trim() == adm.Username.Trim() && Hashing.VerifyHashedPassword(adm.Password.Trim(), admin.Password.Trim()))
                    return isValid;
            }
            return !isValid;
        }
    }
}
