using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectManager_01.Application.Contracts.Authorization;
using ProjectManager_01.Application.DTOs.Users;

namespace ProjectManager_01.Infrastructure.Auth;

internal sealed class JwtGenerator : IJwtGenerator
{
    private readonly IConfiguration _configuration;

    public JwtGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(UserClaimsDto userClaimsDto)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userClaimsDto.Id.ToString()),
            new Claim(ClaimTypes.Role, userClaimsDto.Role)
        };

        foreach (var permission in userClaimsDto.projectPermissions)
        {
            claims.Add(new Claim("ProjectPermission", permission));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(3),
            signingCredentials: creds
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}