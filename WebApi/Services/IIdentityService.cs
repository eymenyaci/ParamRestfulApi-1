using System.Threading.Tasks;


namespace WebApi.Services
{
    public interface IIdentityService
    {
        string Login(string userName,string password);

        bool ValidateToken(string token);
    }
}
