using Microsoft.IdentityModel.Tokens;
using OrderManagement.Common.Contracts;
using OrderManagement.Common.DTO.Authenticate;
using OrderManagement.Common.Helpers;
using OrderManagement.Common.Result;
using OrderManagement.Data.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using OrderManagement.Common.DTO.User;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace OrderManagement.BusinessEngine
{
    public class UserBusinessEngine : IUserBusinessEngine
    {
        private readonly AppSettings _appSettings;
        private readonly IConfiguration _configuration;
        private EGITIM_TESTContext _context;

        public UserBusinessEngine(IOptions<AppSettings> appSettings, EGITIM_TESTContext context, IConfiguration configuration)
        {
            _appSettings = appSettings.Value;
            _context = context;
            _configuration = configuration;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var userPassword = model.Password;
            using (MD5 md5 = MD5.Create())
            {
                userPassword = MD5Helper.GetMd5Hash(md5, userPassword);
            }
            var user = _context.Users.SingleOrDefault(x => x.Username == model.Username && x.Password == userPassword);

            if (user == null) return null;

            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Result<User>> GetById(int id)
        {
            var userById = await _context.Users.FirstAsync(x => x.Id == id);
            return new Result<User> { Data = userById, Message = "Operation successful", Status = true };
        }

        public async Task<Result<User>> CreateUser(UserCreateDto userCreateDto)
        {
            var userPassword = userCreateDto.Password;
            using (MD5 md5 = MD5.Create())
            {
                userPassword = MD5Helper.GetMd5Hash(md5, userCreateDto.Password);
            }

            var user = new User()
            {
                Username = userCreateDto.Username,
                Password = userPassword,
                FirstName = userCreateDto.FirstName,
                LastName = userCreateDto.LastName
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return new Result<User> { Data = user, Message = "Operation successful", Status = true };
        }

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 1 hours
            var tokenHandler = new JwtSecurityTokenHandler();

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("id", user.Id.ToString()));
            claimsForToken.Add(new Claim("userName", user.Username.ToString()));

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials
                );

            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(claimsForToken),
            //    Expires = DateTime.UtcNow.AddDays(7),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};

            //var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(jwtSecurityToken);
        }
    }
}
