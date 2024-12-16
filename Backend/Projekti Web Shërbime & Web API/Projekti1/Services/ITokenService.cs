using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projekti1.User;


public interface ITokenService
{
    Task<string> GenerateRefreshTokenAsync(User user);
    Task<RefreshToken> GetRefreshTokenAsync(string token);
    Task RevokeRefreshTokenAsync(string token);
    string GenerateJwtToken(User user); // Method signature for generating JWT token

}
