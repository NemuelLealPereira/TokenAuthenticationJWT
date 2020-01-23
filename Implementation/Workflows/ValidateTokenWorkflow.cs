using Implementation.Components;
using Implementation.Entities;
using Implementation.Repositories;
using System;

namespace Implementation.Workflows
{
    public class ValidateTokenWorkflow : IValidateTokenWorkflow
    {
        private readonly ITokenManagerComponent _tokenManagerComponent;
        private readonly IUserRepository _userRepository;

        public ValidateTokenWorkflow(ITokenManagerComponent tokenManagerComponent, IUserRepository userRepository)
        {
            _tokenManagerComponent = tokenManagerComponent ?? throw new ArgumentNullException(nameof(tokenManagerComponent));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public User Validate(string token, string username)
        {
            var user = _userRepository.GetUser(username);

            if (user is null)
                return user;

            string tokenUsername = _tokenManagerComponent.ValidateToken(token);

            if (username.Equals(tokenUsername))
                return user;

            return null;

        }
    }
}
