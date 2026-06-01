using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using MediatR;
using Application.Interfaces;
using Core.Auth;

namespace Application.Features.Commands.Auth
{
    public class LoginCommand : IRequest<AuthResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse >
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher _passwordHasher;
        public LoginCommandHandler(IUserRepository userRepository, ITokenService tokenService, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }
        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
                throw new Exception("Invalid credentials");

            if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
                throw new Exception("Invalid credentials");

            var accessToken = _tokenService.CreateToken(user);

            var refreshToken = _tokenService.GenerateRefreshToken();

            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
