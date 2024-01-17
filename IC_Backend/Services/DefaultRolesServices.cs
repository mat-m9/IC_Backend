namespace IC_Backend.Services
{
    public class DefaultRolesServices
    {
        public static readonly Dictionary<string, string> Roles = new Dictionary<string, string>
        {
            {"administrador","ADMINISTRADOR" },
            {"vendedor","VENDEDOR" },
            {"comprador", "COMPRADOR" }
        };

        public Task<string> GetDefaultRole(string key)
        {
            return Task.FromResult(Roles.ContainsKey(key) ? Roles[key] : null);
        }

        public List<string> GetRolesList()
        {
            return Roles.Values.ToList();
        }
    }
}
