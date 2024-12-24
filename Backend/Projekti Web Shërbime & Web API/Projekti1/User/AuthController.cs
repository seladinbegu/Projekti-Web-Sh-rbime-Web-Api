using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace Projekti1.User
{
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService; // Assuming you have an ITokenService

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _tokenService = tokenService; // Initialize the token service
        }

        // Register action
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new User
            {
                UserName = model.Username,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // If the username is in the special list, assign "Admin" role only
                if (new[] { "seladin", "denis", "alban" }.Contains(model.Username.ToLower()))
                {
                    // Ensure the user is only assigned to "Admin" and not "User"
                    if (!await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }

                    // Ensure the user is not assigned the "User" role
                    if (await _userManager.IsInRoleAsync(user, "User"))
                    {
                        await _userManager.RemoveFromRoleAsync(user, "User");
                    }
                }
                else
                {
                    // For all other users, assign only the "User" role
                    if (!await _userManager.IsInRoleAsync(user, "User"))
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                    }

                    // Ensure the user is not assigned the "Admin" role
                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        await _userManager.RemoveFromRoleAsync(user, "Admin");
                    }
                }

                return Ok(new { Message = "User registered successfully." });
            }

            return BadRequest(result.Errors);
        }

        // Login action
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                // Ensure user is found
                if (user == null)
                    return Unauthorized(new { Message = "User not found." });

                // Ensure the user has only one role
                if (new[] { "seladin", "denis", "alban" }.Contains(model.Username.ToLower()))
                {
                    // Assign Admin role and remove User role
                    if (!await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }

                    if (await _userManager.IsInRoleAsync(user, "User"))
                    {
                        await _userManager.RemoveFromRoleAsync(user, "User");
                    }
                }
                else
                {
                    // Assign User role and remove Admin role
                    if (!await _userManager.IsInRoleAsync(user, "User"))
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                    }

                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        await _userManager.RemoveFromRoleAsync(user, "Admin");
                    }
                }

                var token = await GenerateJwtToken(user);
                var refreshToken = await _tokenService.GenerateRefreshTokenAsync(user); // Assuming this method exists

                // Return the tokens and user info
                return Ok(new
                {
                    AccessToken = token,
                    RefreshToken = refreshToken,
                    UserName = user.UserName,
                    Email = user.Email
                });
            }

            return Unauthorized(new { Message = "Invalid login attempt." });
        }


        // Generate JWT token
        private async Task<string> GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:SigninKey"]);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email) // Include email claim
            };

            // Retrieve roles for the user and add them to claims
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1), // Adjust as necessary
                Audience = _configuration["JWT:Audience"], // Set the audience claim
                Issuer = _configuration["JWT:Issuer"], // Set the issuer claim
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // Refresh token action
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest(new { Message = "Refresh token is required." });

            // Retrieve the refresh token from the database
            var storedRefreshToken = await _tokenService.GetRefreshTokenAsync(refreshToken);
            if (storedRefreshToken == null)
                return Unauthorized(new { Message = "Invalid or expired refresh token." });

            // Find the user associated with the refresh token
            var user = await _userManager.FindByIdAsync(storedRefreshToken.UserId);
            if (user == null)
                return Unauthorized(new { Message = "User not found." });

            // Generate a new JWT token and refresh token
            var newJwtToken = await GenerateJwtToken(user); // Use the method to generate new token
            var newRefreshToken = await _tokenService.GenerateRefreshTokenAsync(user); // Assuming this method exists

            return Ok(new
            {
                AccessToken = newJwtToken,
                RefreshToken = newRefreshToken
            });
        }








        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            if (users == null || !users.Any())
            {
                return NotFound(new { Message = "No users found." });
            }

            var userDtos = users.Select(user => new
            {
                user.Id,
                user.UserName,
                user.Email
            }).ToList();

            return Ok(userDtos);
        }



        [HttpGet("users/by-username")]
        public async Task<IActionResult> GetUserByUsername([FromQuery] string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest(new { Message = "Username is required." });
            }

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound(new { Message = $"User with username {username} not found." });
            }

            var userDto = new
            {
                user.Id,
                user.UserName,
                user.Email
            };

            return Ok(userDto);
        }


        [HttpDelete("users/{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteUserById(string id)
        {
            // Find the user by their ID
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }

            // Delete the user
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return Ok(new { Message = "User deleted successfully." });
            }

            return BadRequest(new { Message = "Failed to delete user.", Errors = result.Errors });
        }





        [HttpPut("users/update-username")]
        [Authorize]
        public async Task<IActionResult> UpdateUsername([FromBody] UpdateUsernameModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound(new { Message = "User not found." });

            var existingUser = await _userManager.FindByNameAsync(model.NewUsername);
            if (existingUser != null)
                return BadRequest(new { Message = "Username already taken." });

            user.UserName = model.NewUsername;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Username updated successfully." });
            }

            return BadRequest(new { Message = "Failed to update username.", Errors = result.Errors });
        }




        [HttpPut("users/update-password")]
        [Authorize] // Ensure that only authenticated users can update their password
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Get the current user from the claims
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound(new { Message = "User not found." });

            // Check if the current password is correct
            var passwordCheck = await _userManager.CheckPasswordAsync(user, model.CurrentPassword);
            if (!passwordCheck)
                return Unauthorized(new { Message = "Current password is incorrect." });

            // Update the password
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Password updated successfully." });
            }

            return BadRequest(new { Message = "Failed to update password.", Errors = result.Errors });
        }



        public class UpdateUsernameModel
        {
            public string NewUsername { get; set; }
        }




        public class UpdatePasswordModel
        {
            public string CurrentPassword { get; set; }
            public string NewPassword { get; set; }
        }









    }
}
