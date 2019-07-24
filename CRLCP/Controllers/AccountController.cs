using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

using CRLCP.Helper;
using CRLCP.Models;
using CRLCP.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace CRLCP.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        
        private IUserRepository _userRepo;
        private readonly JsonResponse jsonResponse;
        private readonly AppSettings _appSettings;
        private CLRCP_MASTERContext _masterContext;

        public AccountController(CLRCP_MASTERContext context, IUserRepository userService, IOptions<AppSettings> appSettings, JsonResponse jsonResponse)
        {
            _masterContext = context;
            _userRepo = userService;
            this.jsonResponse = jsonResponse;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Register([FromBody]LoginDetails user)
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
                await _masterContext.SaveChangesAsync();
                return Created("", user.UserId);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof((bool IsAuthenticate, string Token, int UserType)), StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public IActionResult Login([FromBody] LoginModel login)
        {
            var user =  _userRepo.Authenticate(login);
            if (user == null)
                return Unauthorized((
                    IsAuthenticate: false,
                    Token: string.Empty,
                    UserType: 0
                )) ;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult Update([FromBody] UserInfo userInfo)
        {
            try
            {
                UserInfo user = _masterContext.UserInfo.FirstOrDefault(x=>x.UserId==userInfo.UserId);
                if (user == null)
                {
                    return NotFound(jsonResponse.Response="User Not Found");
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
                    try
                    {
                        _masterContext.UserInfo.Update(user);
                        _masterContext.SaveChanges();
                        jsonResponse.IsSuccessful = true;
                        jsonResponse.Response = "Profile Update successfully.";
                        return Ok(jsonResponse);
                    }
                    catch (Exception)
                    {
                        jsonResponse.IsSuccessful = false;
                        jsonResponse.Response = "Internal Exception.";
                        return BadRequest(jsonResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(jsonResponse.Response = "Unable to Update Profile");
            }
        }

       
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonResponse), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetProfile(int UserId)
        {
            return Ok(_masterContext.UserInfo.FirstOrDefault(x => x.UserId == UserId));
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