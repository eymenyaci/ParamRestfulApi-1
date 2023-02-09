using System.Threading.Tasks;


namespace WebApi.Interfaces
{
    public interface IIdentityService
    {
        string Login(string userName, string password);

        bool ValidateToken(string token);
    }
}
