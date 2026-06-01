using Core.Auth;
using Core.Entities;
using Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.Auth
{
    public class RefreshCommand: IRequest<AuthResponse>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    public class RefreshCommandHandler : IRequestHandler<RefreshCommand, AuthResponse>
    {
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public RefreshCommandHandler(ITokenService tokenService, IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository, IConfiguration configuration)
        {
            _tokenService = tokenService;
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _configuration = configuration;
        }


        public async Task<AuthResponse> Handle(RefreshCommand request, CancellationToken cancellationToken)
        {
            var principal = GetPrincipalFromExpiredToken(request.AccessToken);

            var userId = int.Parse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var storedToken = await _refreshTokenRepository.GetValidToken(request.RefreshToken, userId);

            if (storedToken == null || storedToken.Expires < DateTime.UtcNow)
                throw new Exception("Invalid refresh token");

            var user = await _userRepository.GetByIdAsync(userId);

            var newAccessToken = _tokenService.CreateToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            storedToken.IsRevoked = true;

            await _refreshTokenRepository.AddAsync(new RefreshToken
            {
                Token = newRefreshToken,
                UserId = userId,
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            });

            await _refreshTokenRepository.SaveChangesAsync();

            return new AuthResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };


        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
        }
    }
}
