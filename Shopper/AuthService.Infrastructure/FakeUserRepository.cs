using AuthService.Domain;
using Bogus;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Infrastructure
{
    public class FakeUserRepository : IUserRepository
    {
        private readonly IEnumerable<User> users;

        public FakeUserRepository(Faker<User> faker)
        {
            users = faker.Generate(100);
        }

        public Task<User> Get(string login)
        {            
            return Task.FromResult(users.SingleOrDefault(p => p.Login == login));
        }
    }

    public class MyAuthorizationService : IAuthorizationService
    {
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher<User> passwordHasher;

        public MyAuthorizationService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
        }

        public bool TryAuthorize(string login, string password, out User user)
        {
            user = userRepository.Get(login).Result;

            if (user == null)
                return false;

            return passwordHasher.VerifyHashedPassword(user, user.HashedPassword, password) == PasswordVerificationResult.Success;


        }
    }
}
