using JWT.API.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT.API.Utilities
{
    public class TokenCreater
    {
        private readonly TokenOptions _options;

        public TokenCreater(IOptions<TokenOptions> options)
        {
            _options = options.Value;
        }


        public string CreateMemberRoleToken()
        {
            var signingCredentials = CreateSignInCredential();

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,"Member"),
                new Claim(ClaimTypes.Role,"Editor"),
                new Claim(ClaimTypes.Name,"MemberUserName"),
            };

            return CreateToken(claims: claims, signingCredentials: signingCredentials);
        }

        public string CreateAdminRoleToken()
        {
            var signingCredentials = CreateSignInCredential();

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim(ClaimTypes.Name,"AdminUserName")
            };

            return CreateToken(claims: claims, signingCredentials: signingCredentials);
        }

        private SigningCredentials CreateSignInCredential()
        {
            var codingKey = Encoding.UTF8.GetBytes(_options.Key);

            SymmetricSecurityKey key = new SymmetricSecurityKey(codingKey);

            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return signingCredentials;
        }

        private string CreateToken(IList<Claim> claims, SigningCredentials signingCredentials)
        {
            JwtSecurityToken securityToken = new JwtSecurityToken
             (
                  issuer: _options.Isssuer,
                  audience: _options.Audience,
                  notBefore: DateTime.Now,
                  expires: DateTime.Now.AddHours(_options.ExpireTime),
                  signingCredentials: signingCredentials,
                  claims: claims
             );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(securityToken);

            return token;
        }

    }
}
