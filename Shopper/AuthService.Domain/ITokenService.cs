namespace AuthService.Domain
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
