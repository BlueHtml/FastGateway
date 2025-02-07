﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FastGateway.Options;
using Microsoft.IdentityModel.Tokens;

namespace FastGateway.Services;

public static class AuthorizeService
{
    public static ResultDto CreateAsync(AuthorizeInput input)
    {
        // 获取环境变量
        var password = Environment.GetEnvironmentVariable("PASSWORD") ?? "Aa123456";

        if (input.Password != password)
        {
            return ResultDto.ErrorResult("密码错误");
        }

        // 创建一个JWT Token
        var claims = new List<Claim>
        {
            new(ClaimTypes.Role, "admin"),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(JwtOptions.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            Audience = "FastGateway",
            Issuer = "FastGateway",
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return ResultDto.SuccessResult(tokenHandler.WriteToken(token));
    }
}