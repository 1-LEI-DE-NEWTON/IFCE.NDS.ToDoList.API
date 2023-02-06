using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NDS_ToDo.Application.Configuration;
using NDS_ToDo.Application.DTOs.Auth;
using NDS_ToDo.Application.DTOs.User;
using NDS_ToDo.Application.Notifications;
using NDS_ToDo.Application.Services.Contracts;
using NDS_ToDo.Domain.Contracts.Repository;
using NDS_ToDo.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IFCE.NDS.ToDoList.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppSettings _appSettings;
        private readonly INotificator _notificator;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthService(IUserRepository userRepository, INotificator notificator, IPasswordHasher<User> passwordHasher, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _notificator = notificator;
            _passwordHasher = passwordHasher;
            _appSettings = appSettings.Value;
        }

        public async Task<TokenDto> Login(LoginDto model)
        {
            var user = await _userRepository.FindByEmail(model.Email);
            if (user == null)
            {
                _notificator.Handle(new Notifications("Usuário e/ou Password incorretos!"));
                return null;
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);
            if (result == PasswordVerificationResult.Success)
            {
                return GerarToken(user);
            }

            _notificator.Handle(new Notifications("Usuário e/ou Password incorretos!"));
            return null;
        }

        public async Task<TokenDto> Register(RegisterDto model)
        {
            if (await _userRepository.IsEmailInUse(model.Email))
            {
                _notificator.Handle(new Notifications("Email já cadastrado!"));
                return null;
            }

            var user = new User
            {
                Name = model.Name,
                Email = model.Email
            };
            user.Password = _passwordHasher.HashPassword(user, model.Password);

            _userRepository.Add(user);

            if (await _userRepository.UnitOfWork.Commit())
                return GerarToken(user);

            _notificator.Handle(new Notifications("Ocorreu um erro ao salvar usuário"));
            return null;
        }

        private TokenDto GerarToken(User user)
        {
            var claims = new List<Claim>
        {
            new ("Id", user.Id.ToString()),
            new (JwtRegisteredClaimNames.Name, user.Name),
            new (JwtRegisteredClaimNames.Email, user.Email),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new (JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()),
            new (JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString())
        };

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience,
                Expires = DateTime.UtcNow.AddHours(_appSettings.Expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret)),
                    SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            return new TokenDto
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.Expiration).TotalSeconds,
                User = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email
                }
            };
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalMilliseconds);

    }
}
