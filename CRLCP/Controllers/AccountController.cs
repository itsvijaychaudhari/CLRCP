using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

using CRLCP.Helper;
using CRLCP.Models;
using CRLCP.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CRLCP.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUserRepository _userRepo;
        private readonly AppSettings _appSettings;
        private CLRCP_MASTERContext _masterContext;

        public AccountController(CLRCP_MASTERContext context, IUserRepository userService, IOptions<AppSettings> appSettings)
        {
            _masterContext = context;
            _userRepo = userService;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register([FromBody]LoginDetails user)
        {
            try
            {
                _userRepo.Create(user);
               
                _masterContext.UserInfo.Add(
                    new UserInfo
                    {
                        UserId = user.UserId,
                        Name = string.Empty,
                        Age = 0,
                        Gender = string.Empty,
                        LangId1 = 1,//TODO
                        LangId2 = -1,
                        LangId3 = -1,
                        QualificationId = -1

                    });
                _masterContext.SaveChanges();
                return Created("", user.UserId);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] LoginModel login)
        {
            var user = _userRepo.Authenticate(login);
            if (user == null)
                return Unauthorized(new
                {
                    IsAuthenticate = false,
                    Token = string.Empty,
                    UserType = 0
                }) ;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, login.EmailId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(new
            {
                IsAuthenticate = true,
                Token = tokenString,
                UserId = user.UserId,
                UserType = user.UserType
            });

        }

        [HttpPost]
        public string Update([FromBody] UserInfo userInfo)
        {
            try
            {
                UserInfo user = _masterContext.UserInfo.FirstOrDefault(x=>x.UserId==userInfo.UserId);
                if (user == null)
                {
                    return "User Not Found";
                }
                else
                {
                    user.Name = userInfo.Name;
                    user.Age = userInfo.Age;
                    user.Gender = userInfo.Gender;
                    user.LangId1 = userInfo.LangId1;
                    user.LangId2 = userInfo.LangId2;
                    user.LangId3 = userInfo.LangId3;
                    user.QualificationId = userInfo.QualificationId;
                   
                    _masterContext.UserInfo.Update(user);
                    _masterContext.SaveChanges();
                    return "Profile Update successfully.";
                }
            }
            catch (Exception ex)
            {
                return "Unable to Update Profile";
            }
        }

       
        [HttpGet]
        public IActionResult GetProfile(int UserId)
        {
            try
            {
                return Ok(_masterContext.UserInfo.FirstOrDefault(x => x.UserId == UserId));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        

        [HttpGet]
        public IEnumerable<LanguageIdMapping> GetLanguages()
        {
            return _masterContext.LanguageIdMapping.ToList();
        }

       
        [HttpGet]
        public IEnumerable<QualificationIdMapping> GetQualifications()
        {
            return _masterContext.QualificationIdMapping.ToList();
        }

    }
}