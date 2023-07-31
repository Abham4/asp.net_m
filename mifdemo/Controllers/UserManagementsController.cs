using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using mifdemo.ViewModels.Authentication;
using Mifdemo.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Mifdemo.Domain.Interface.ServiceInterface;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace mifdemo.Controllers
{
    [ApiController]
    [Route("api/User-Account")]
    public class UserManagementsController : ControllerBase
    {
        private readonly UserManager<ApplicationUserModel> userManager;
        private readonly ITokenManagerService _tokenManager;

        // private readonly IPasswordHasher<ApplicationUserModel> _passwordHasher;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostEnv;

        public UserManagementsController(IWebHostEnvironment hostEnvironment, ITokenManagerService service, UserManager<ApplicationUserModel> user, RoleManager<IdentityRole> role, IConfiguration configuration)
        {
            userManager = user;
            roleManager = role;
            _configuration = configuration;
            _tokenManager = service;
            _hostEnv = hostEnvironment;
        }

        [HttpGet(Name = "Users")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<List<ApplicationUserModel>>> GetUsers()
        {
            return await userManager.Users
                .Include(c => c.Branch)
                .ToListAsync();
        }

        // [HttpGet("{id}")]
        // [Authorize(Roles = UserRoles.Admin)]
        // public async Task<ActionResult<ApplicationUserModel>> GetUser(string id)
        // {
        //    var user =  await _con.Users.FindAsync(id);

        //    if (user == null)
        //     return NotFound();
        //    return user;
        // }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);  
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))  
            {  
                var userRoles = await userManager.GetRolesAsync(user);  
  
                var authClaims = new List<Claim>  
                {  
                    new Claim("FirstName", user.FirstName),  
                    new Claim("BranchId", user.BranchId.ToString()),  
                    new Claim("LastName", user.LastName),  
                    new Claim("PicURL", user.ProfileImg),  
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),  
                };  
  
                foreach (var userRole in userRoles)  
                {  
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));  
                }  
  
                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
                
                var token = CreateToken(authClaims);

                var refreshToken = GenerateRefreshToken();

                var tokenManager = new TokenManagerModel()
                {
                    UserId = user.Id,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays)
                };

                await _tokenManager.PostTokenAsync(tokenManager);
                   
                // Response.Cookies.Append("JWT", token.ToString(), new CookieOptions{
                //     HttpOnly = true
                // });
  
                // return Ok(new Response{ Status = "Success", Message = "Login Successfull."} );  
                return Ok(new {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    refreshToken = refreshToken
                });
            }  
            return Unauthorized();
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[128];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        [HttpPost]
        [Route("Refresh-Token")]
        public async Task<IActionResult> RefreshToken(Refresh refreshToken)
        {
            if(refreshToken.RefreshToken == null)
                return BadRequest("Invalid Refresh Token");
            
            var userId = await _tokenManager.GetUserId(refreshToken.RefreshToken);

            if(userId == null)
                return BadRequest("Invalid Refresh Token");

            if(userId.Used)
                return BadRequest("Blacklisted Refresh Token Access Denied!");

            var user = await userManager.FindByIdAsync(userId.UserId);

            if(user == null || userId.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid Refresh Token");

            var userRoles = await userManager.GetRolesAsync(user);  
  
            var authClaims = new List<Claim>  
            {  
                new Claim("FirstName", user.FirstName),  
                new Claim("BranchId", user.BranchId.ToString()),
                new Claim("LastName", user.LastName),  
                new Claim("PicURL", user.ProfileImg),  
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),  
            };  

            foreach (var userRole in userRoles)  
            {  
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));  
            }

            userId.Used = true;

            await _tokenManager.PutTokenAsync(userId);

            var newToken = CreateToken(authClaims);

            var newRefresh = GenerateRefreshToken();

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

            var tokenManager = new TokenManagerModel()
            {
                UserId = user.Id,
                RefreshToken = newRefresh,
                RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays)
            };

            await _tokenManager.PostTokenAsync(tokenManager);

            return new ObjectResult(
                new{
                    token = new JwtSecurityTokenHandler().WriteToken(newToken),
                    refreshToken = newRefresh
                }
            );
        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));  

            _ = double.TryParse(_configuration["JWT:TokenValidityInMinute"], out double tokenValidityInMinute);

            var token = new JwtSecurityToken(  
                notBefore: DateTime.Now,
                issuer: _configuration["JWT:ValidIssuer"],  
                audience: _configuration["JWT:ValidAudience"],  
                expires: DateTime.Now.AddHours(tokenValidityInMinute),  
                claims: authClaims,  
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)  
            );

            return token;
        }

        // private JwtSecurityToken Verify(string jwt)
        // {
        //     Console.WriteLine(jwt);

        //     var tokenHandler = new JwtSecurityTokenHandler();
        //     var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);
            
        //     tokenHandler.ValidateToken(jwt, new TokenValidationParameters{
        //             ValidateIssuer = false,
        //             ValidateAudience = false,
        //             // ValidAudience = _configuration["JWT:ValidAudience"],
        //             // ValidIssuer = _configuration["JWT:ValidIssuer"],
        //             ValidateIssuerSigningKey = true,
        //             IssuerSigningKey = new SymmetricSecurityKey(key)
        //     }, out SecurityToken validateToken);

        //     return (JwtSecurityToken)validateToken;
        // }

        // [HttpGet("User")]
        // public new IActionResult User()
        // {
        //     try
        //     {
        //         var jwt = Request.Cookies["JWT"];

        //         //Console.WriteLine(jwt);

        //         var token = Verify(jwt);

        //         Console.WriteLine(token);

        //         var userId = token.Issuer;

        //         var user = _con.Users.SingleOrDefaultAsync(c => c.Email == userId);

        //         return Ok(
        //             user
        //         );
        //     }
        //     catch (Exception e)
        //     {
        //         Console.WriteLine(e);
        //         return Unauthorized();
        //     }
            
        // }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterModel model)
        {
            var userExists = await userManager.FindByEmailAsync(model.Email);  
            if (userExists != null)  
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });  

            if(model.Img != null)
                model.ProfileImg = await SaveFile(model.Img);
            else
                model.ProfileImg = "No Image";
            
            ApplicationUserModel user = new ApplicationUserModel()  
            {  
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,  
                SecurityStamp = Guid.NewGuid().ToString(),  
                UserName = model.Email,
                PhoneNumber = model.PhoneNumber,
                BranchId = model.BranchId,
                ProfileImg = model.ProfileImg
            };  
            var result = await userManager.CreateAsync(user, model.Password);  
            if (!result.Succeeded)  
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });  

            if (await roleManager.RoleExistsAsync(UserRoles.User))  
            {  
                await userManager.AddToRoleAsync(user, UserRoles.User);  
            } 

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });  
        }
        [HttpPost]
        [Route("Register-Admin")]
        public async Task<IActionResult> RegisterAdmin([FromForm] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Email);  
            if (userExists != null)  
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });  

            if(model.Img != null)
                model.ProfileImg = await SaveFile(model.Img);
            else
                model.ProfileImg = "No Image";
            
            ApplicationUserModel user = new ApplicationUserModel()  
            {  
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,  
                SecurityStamp = Guid.NewGuid().ToString(),  
                UserName = model.Email,
                PhoneNumber = model.PhoneNumber,
                BranchId = model.BranchId,
                ProfileImg = model.ProfileImg
            };  
            var result = await userManager.CreateAsync(user, model.Password);  
            if (!result.Succeeded)  
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });  
  
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))  
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));  
            if (!await roleManager.RoleExistsAsync(UserRoles.User))  
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));  
  
            if (await roleManager.RoleExistsAsync(UserRoles.Admin))  
            {  
                await userManager.AddToRoleAsync(user, UserRoles.Admin);  
            }  
  
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });  

        }

        [NonAction]
        public async Task<string> SaveFile(IFormFile xFile)
        {
            string xName = new String(Path.GetFileNameWithoutExtension(xFile.FileName)
                .Take(10)
                .ToArray())
                .Replace(' ', '_');
            xName = "Files/User_Pics/" + xName + '-' + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(xFile.FileName);
            var imagePath = Path.Combine(_hostEnv.ContentRootPath, xName);
            using(var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await xFile.CopyToAsync(fileStream);
            }
            return xName;
        }
    }
}
