using FindABook.Models;
using FindABook.Models.UtillityModels;
using FindABook.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FindABook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly BooksDbContext _context;
        private readonly JwtSettings _jwtSettings;
        private readonly byte[] _key;
        public UserAuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, BooksDbContext context, JwtSettings jwtSettings)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._context = context;
            this._jwtSettings = jwtSettings;
            _key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
        }
        [Route("Login")]
        [HttpPost]
        public async Task<object> Login(LoginViewModel model)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(model.Username);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (result.Succeeded)
                {

                    var roleIds = _context.UserRoles.Where(a => a.UserId == user.Id).Select(a => a.RoleId).ToList();

                    List<IdentityRole> roles = new List<IdentityRole>();
                    foreach (var roleId in roleIds)
                    {
                        IdentityRole role = _context.Roles.Where(a => a.Id == roleId).FirstOrDefault();
                        if (role != null)
                        {
                            roles.Add(role);
                        }
                    }

                    var tokenDescriptor = GenerateTokenDescriptor(user, new { roles, model });

                    HttpContext httpContext = Response.HttpContext;
                    return new { sucess = true, token = tokenDescriptor, userId = user.Id, contactId = _context.Users.Where(a => a.Id == user.Id).FirstOrDefault()?.Id };

                }
                return new { message = "", success = false };
            }
            else { return new { message = "Incorrect Username or Password", success = false }; }
        }
        private string GenerateTokenDescriptor(IdentityUser user, object info)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim("ContactId",Convert.ToString(_context.Users.Where(a => a.Id == user.Id).FirstOrDefault()?.Id)),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, user.Id),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                    new System.Security.Claims.Claim("AdditionalInfo", JsonConvert.SerializeObject(info)),
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private bool CheckUsername(RegisterViewModel model)
        {
            return (model.Username == model.EmailAddress || model.Username == model.MobileNumber);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<object> Register(RegisterViewModel model)
        {
            IdentityResult result = null;

            if (ModelState.IsValid)
            {
                bool check = true;
                if (model.UsernameFromEmailOrMobile)
                {
                    check = CheckUsername(model);
                }

                if (check)
                {
                    var user = new ApplicationUser() { Email = model.EmailAddress, UserName = model.Username, PhoneNumber = model.MobileNumber };
                    try
                    {
                        result = await _userManager.CreateAsync(user, model.Password);

                        if (result.Succeeded)
                        {
                            List<IdentityRole> roles = new List<IdentityRole>();
                            using (var dbContextTransaction = _context.Database.BeginTransaction())
                            {
                                _context.UserRoles.Add(new IdentityUserRole<string>
                                {
                                    RoleId = "1",
                                    UserId = user.Id
                                });
                                _context.SaveChanges();
                                roles.Add(_context.Roles.Where(a => a.Id == "1").FirstOrDefault());
                                _context.Database.CommitTransaction();
                                dbContextTransaction.Dispose();
                                await _signInManager.SignInAsync(user, false);
                            }
                            var tokenDescriptor = GenerateTokenDescriptor(user, "");
                            return new { success = true, token = tokenDescriptor };
                        }
                        else
                        {
                            return new { message = result.Errors, success = false };
                        }
                    }
                    catch (DbUpdateException e)
                    {
                        string message = e.InnerException.Message;
                        string inUseEmail = "The email provided is already in use";
                        string inUseMobile = "The phone number provided is already in use";
                        return new { message = message.ToLower().Contains("email") ? inUseEmail : message.ToLower().Contains("phone") ? inUseMobile : "Something is wrong. Please contact support.", success = false };
                    }
                }
                else
                {
                    return new { message = "Username must be the same as email address or mobile number", success = false };
                }
            }
            else
                return new { message = JsonConvert.SerializeObject(ModelState.Values), success = false };
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<object> CreateContact(RegisterViewModel contact)
        {
            IdentityResult result = null;

            if (ModelState.IsValid)
            {
                bool check = true; // CheckUsername(model);



                if (check)
                {
                    var user = new ApplicationUser() { Email = contact.EmailAddress, UserName = contact.EmailAddress, PhoneNumber = contact.MobileNumber };
                    try
                    {
                        result = await _userManager.CreateAsync(user, contact.Password);

                        if (result.Succeeded)
                        {
                            List<IdentityRole> roles = new List<IdentityRole>();
                            using (var dbContextTransaction = _context.Database.BeginTransaction())
                            {
                                _context.UserRoles.Add(new IdentityUserRole<string>
                                {
                                    RoleId = "1",
                                    UserId = user.Id
                                });
                                _context.SaveChanges();
                                _context.Database.CommitTransaction();
                                dbContextTransaction.Dispose();
                                await _signInManager.SignInAsync(user, false);
                            }
                            var tokenDescriptor = GenerateTokenDescriptor(user, "");
                            return new { success = true};
                        }
                        else
                        {
                            return new { message = result.Errors, success = false };
                        }
                    }
                    catch (DbUpdateException e)
                    {
                        string message = e.InnerException.Message;
                        string inUseEmail = "The email provided is already in use";
                        string inUseMobile = "The phone number provided is already in use";
                        return new { message = message.ToLower().Contains("email") ? inUseEmail : message.ToLower().Contains("phone") ? inUseMobile : "Something is wrong. Please contact support.", success = false };
                    }
                }
                else
                {
                    return new { message = "Username must be the same as email address or mobile number", success = false };
                }
            }
            else
                return new { message = JsonConvert.SerializeObject(ModelState.Values), success = false };
        }

        private object UpdateUser(RegisterViewModel contact)
        {
            ApplicationUser user = _context.Users.FirstOrDefault(a => a.UserName == contact.Username);
            user.Email = contact.EmailAddress;
            user.UserName = contact.EmailAddress;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChangesAsync();

            return contact;
        }
    }
}
