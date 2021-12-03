using AuthService.Domain;
using Bogus;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Infrastructure
{
    public class UserFaker : Faker<User>
    {
        public UserFaker(IPasswordHasher<User> hasher)
        {
            UseSeed(1);

            RuleFor(p => p.Login, f => f.Person.UserName);
            RuleFor(p => p.FirstName, f => f.Person.FirstName);
            RuleFor(p => p.LastName, f => f.Person.LastName);
            RuleFor(p => p.Email, f => f.Person.Email);
            RuleFor(p => p.PhoneNumber, f => f.Person.Phone);
            RuleFor(p => p.HashedPassword, (f,user) => hasher.HashPassword(user, "12345"));
        }
    }
}
