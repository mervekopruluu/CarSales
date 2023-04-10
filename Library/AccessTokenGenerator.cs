using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Public.Dtos.UserManagement;
using Repository;
using Repository.Contracts;

namespace Library;

public class AccessTokenGenerator
{
    private IRepositoryManager RepositoryManager { get; set; }
    private IConfiguration Config { get; set; }
    private User User { get; set; }

    public AccessTokenGenerator(IRepositoryManager repositoryManager,
        IConfiguration config,
        User user)
    {
        Config = config;
        RepositoryManager = repositoryManager;
        User = user;
    }

    public async Task<ApplicationUserToken> GetToken()
    {
        var userToken = await RepositoryManager.ApplicationUserTokens.Get(User.Id);
        if (userToken is null)
        {
            var tokenInfo = GenerateToken();
            var applicationUserToken = await RepositoryManager.ApplicationUserTokens.Add(new ApplicationUserToken
            {
                UserId = User.Id,
                LoginProvider = "SystemAPI",
                Name = User.UserName,
                ExpireDate = tokenInfo.ExpireDate,
                Value = tokenInfo.Token
            });

            await RepositoryManager.SaveAsync();
            return applicationUserToken;
        }

        if (userToken.ExpireDate <= DateTime.Now)
        {
            var tokenInfo = GenerateToken();

            userToken.ExpireDate = tokenInfo.ExpireDate;
            userToken.Value = tokenInfo.Token;

            RepositoryManager.ApplicationUserTokens.Update(userToken);

            await RepositoryManager.SaveAsync();
        }

        return userToken;
    }

    public async Task DeleteToken()
    {
        RepositoryManager.ApplicationUserTokens.Delete(User.Id);
        await RepositoryManager.SaveAsync();
    }

    private TokenInfoDto GenerateToken()
    {
        var expireDate = DateTime.Now.AddMinutes(10);
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Config["Application:Secret"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = Config["Application:Audience"],
            Issuer = Config["Application:Issuer"],
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.NameIdentifier, User.Id),
                new(ClaimTypes.Name, User.UserName),
            }),
            Expires = expireDate,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        var tokenInfo = new TokenInfoDto
        {
            Token = tokenString,
            ExpireDate = expireDate
        };

        return tokenInfo;
    }
}