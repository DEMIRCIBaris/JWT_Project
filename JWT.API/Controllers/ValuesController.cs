using JWT.API.Models;
using JWT.API.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly TokenCreater _tokenCreater;

        public ValuesController(TokenCreater tokenCreater)
        {
            _tokenCreater = tokenCreater;
        }

        [HttpPost("[action]")]
        public IActionResult MemberLogin([FromForm] LoginModel model)
        {
            if ((model.UserName == "Barış") && (model.Password == "Demirci"))
                return Created("", _tokenCreater.CreateMemberRoleToken());

            return BadRequest("No Auth");
        }

        [HttpPost("[action]")]
        public IActionResult AdminLogin([FromForm] LoginModel model)
        {
            if ((model.UserName == "Berkay") && (model.Password == "Demirci"))
                return Created("", _tokenCreater.CreateAdminRoleToken());

            return BadRequest("No Auth");
        }


        [HttpPost("[action]-Admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult Addition([FromForm] Calculator cal)
        {
            var returnNumber = cal.Number1 + cal.Number2;
            return Ok(returnNumber);
        }

        [HttpPost("[action]-Admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult Subtraction([FromForm] Calculator cal)
        {
            var returnNumber = cal.Number1 - cal.Number2;
            return Ok(returnNumber);
        }

        [HttpPost("[action]-Member")]
        [Authorize(Roles = "Member")]
        public IActionResult Division([FromForm] Calculator cal)
        {
            if (cal.Number2 == 0)
                return BadRequest("You cannot give 0");

            float returnNumber = (float)cal.Number1 / (float)cal.Number2;
            return Ok(returnNumber);
        }

        [HttpPost("[action]-Member")]
        [Authorize(Roles = "Member")]
        public IActionResult Multiplication([FromForm] Calculator cal)
        {
            var returnNumber = cal.Number1 * cal.Number2;
            return Ok(returnNumber);
        }
    }
}
