﻿using DATA.Entities;
using DATA.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Server.Jwt
{
    public static class TokenService
    {
        public static UserToken GenerateToken(User user, IConfiguration config)
        {
            var expire = DateTime.UtcNow.AddSeconds(Convert.ToInt32(config["Jwt:Expiration"]));
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Secret"]));
            var identity = new ClaimsIdentity(
                        new GenericIdentity(user.Username),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //jti 0 id do token
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                            new Claim(ClaimTypes.Expired, expire.ToString()),
                            new Claim(ClaimTypes.Role, user.Role),
                            new Claim(ClaimTypes.Expiration, expire.ToString())
                        }
                    );

            var token = CreateToken(identity, expire, tokenHandler, config, key);
            return new UserToken() { Token = "bearer " + token, Expiration = expire , Username = user.Username, Role = user.Role};
        }

        private static string CreateToken(ClaimsIdentity identity, DateTime expirationDate, JwtSecurityTokenHandler handler, IConfiguration config, SymmetricSecurityKey key)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = config["Jwt:Issuer"],
                Audience = config["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                Subject = identity,
                NotBefore = DateTime.Now,
                Expires = expirationDate,
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }
    }
}
