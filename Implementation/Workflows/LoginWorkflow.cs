using Implementation.Components;
using Implementation.Entities;
using Implementation.Repositories;
using System;

namespace Implementation.Workflows
{
    public class LoginWorkflow : ILoginWorkflow
    {
        private readonly ITokenManagerComponent _tokenManagerComponent;
        private readonly IUserRepository _userRepository;

        public LoginWorkflow(ITokenManagerComponent tokenManagerComponent, IUserRepository userRepository)
        {
            _tokenManagerComponent = tokenManagerComponent ?? throw new ArgumentNullException(nameof(tokenManagerComponent));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public string Login(User user)
        {
            User userRepository = _userRepository.GetUser(user.Username);

            if (userRepository is null)
                return string.Empty;

            bool credentials = userRepository.Password.Equals(user.Password);

            if (!credentials)
                return string.Empty;

            var token = _tokenManagerComponent.GenerateToken(user.Username);

            return token;
        }
    }
}
