using System.Threading.Tasks;

namespace AuthService.Domain
{
    public interface IAuthorizationService
    {
        bool TryAuthorize(string login, string password, out User user);
    }
}
