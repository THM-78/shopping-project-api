using IPE.SmsIrClient;
using IPE.SmsIrClient.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using shopping_project.Data.Entities;
using shopping_project_api.Data.Models.ViewModels;
using shopping_project_api.services;
using System.Data;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace shopping_project_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthUserController : ControllerBase
    {
        private readonly ShoppingContext _context;
        private readonly ITokenService _tokenService;
        public AuthUserController(ITokenService tokenService)
        {
            _context = new ShoppingContext();
            _tokenService = tokenService;
        }


        [HttpPost("SendverificationCode")]
        public async Task<IActionResult> SendCode(AuthUser user)
        {
            try
            {
                SmsIr smsIr = new SmsIr("WwlbBiN5Nb96X6KuBShVGZc0NGA8BWP2aJ2OZ08psJTpLtcgSg463JKUnzUlvfbW");
                Random generator = new Random();
                String r = generator.Next(0, 10000).ToString("D4");

                var verificationSendResult = await smsIr.VerifySendAsync(user.phoneNumber,
                    100000,
                    new VerifySendParameter[] 
                    {
                        new VerifySendParameter("Code", r)
                    });
                if(verificationSendResult.Message == "موفق")
                {
                    VerificationResult result = new VerificationResult
                    {
                        messageId = verificationSendResult.Data.MessageId,
                        phoneNumber = user.phoneNumber

                    };
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(IPE.SmsIrClient.Exceptions.LogicalException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("VerifyUser")]
        public async Task<IActionResult> VerifyUser(VerificationResult result)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("x-api-key", "WwlbBiN5Nb96X6KuBShVGZc0NGA8BWP2aJ2OZ08psJTpLtcgSg463JKUnzUlvfbW");
            var response = await httpClient.GetAsync("https://api.sms.ir/v1/send/"+result.messageId);
            string resultt = await response.Content.ReadAsStringAsync();
            string pattern = "\"mobile\":(\\d+)";
            string pattern2 = "\"messageText\":\"\\s*کد تایید\\s*(\\d+)\\s*\"";
            Match match = Regex.Match(resultt, pattern);
            Match match2 = Regex.Match(resultt, pattern2);

            if (match.Success && match2.Success)
            {
                string mobileNumber = ("0"+long.Parse(match.Groups[1].Value)).ToString();
                string verificationCode = match2.Groups[1].Value;
                if (mobileNumber == result.phoneNumber && verificationCode == result.code)
                {
                    var getuser = await _context.TblUsers.SingleOrDefaultAsync(i => i.Tell == mobileNumber);
                    if (getuser != null)
                    {
                        Claim UserId = new Claim(ClaimTypes.NameIdentifier, getuser.Id.ToString());
                        Claim Role = new Claim(ClaimTypes.Role, _context.TblRoles.SingleOrDefault(i => i.Id == getuser.RoleId).Name);
                        Claim Tell = new Claim(ClaimTypes.MobilePhone, getuser.Tell.ToString());
                        List<Claim> claims = new List<Claim>{Role, UserId , Tell };
                        var token = _tokenService.GenerateToken(claims);
                        var res = new Dictionary<string, string>
                        {
                            { "message", "فرآیند ورود با موفقیت انجام شد" },
                            { "token", _tokenService.GenerateToken(claims) }
                        };
                        return Ok(res);
                    }
                    else
                    {
                        await _context.TblUsers.AddAsync(new TblUser { Tell = mobileNumber });
                        await _context.SaveChangesAsync();
                        Claim Tell = new Claim(ClaimTypes.MobilePhone, mobileNumber);
                        List<Claim> claims = new List<Claim> { Tell };
                        return Ok(
                            new Dictionary<string, string>
                        {
                            { "message", "فرآیند ورود با موفقیت انجام شد" },
                            { "token", _tokenService.GenerateToken(claims) }
                        });
                    }
                }
            }
            return Unauthorized();
        }
    }
}
