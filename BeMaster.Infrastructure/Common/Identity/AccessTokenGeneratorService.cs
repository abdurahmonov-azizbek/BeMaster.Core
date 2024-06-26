﻿// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by Abdurahmonov-azizbek
// --------------------------------------------------------

using BeMaster.Application.Common.Identity;
using BeMaster.Domain.Common.Settings;
using BeMaster.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BeMaster.Infrastructure.Common.Identity
{
    public class AccessTokenGeneratorService(IOptions<JwtSettings> jwtSettings) : IAccessTokenGeneratorService
    {
        private JwtSettings _jwtSettings = jwtSettings.Value;

        public string GetToken(User user)
        {
            var jwtToken = GetJwtToken(user);
            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return token;
        }

        private JwtSecurityToken GetJwtToken(User user)
        {
            var claims = GetClaims(user);

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                issuer: _jwtSettings.ValidIssuer,
                audience: _jwtSettings.ValidAudience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationTimeInMinutes),
                signingCredentials: credentials);
        }

        private List<Claim> GetClaims(User user)
        {
            return new()
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(ClaimConstants.UserId, user.Id.ToString())
        };
        }

    }

}
