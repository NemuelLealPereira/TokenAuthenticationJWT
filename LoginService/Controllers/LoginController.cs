using Implementation.Entities;
using Implementation.Workflows;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LoginService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginWorkflow _loginWorkflow;
        private readonly IValidateTokenWorkflow _validateTokenWorkflow;


        public LoginController(ILoginWorkflow loginWorkflow, IValidateTokenWorkflow validateTokenWorkflow)
        {
            _loginWorkflow = loginWorkflow ?? throw new ArgumentNullException(nameof(loginWorkflow));
            _validateTokenWorkflow = validateTokenWorkflow ?? throw new ArgumentNullException(nameof(validateTokenWorkflow));
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            try
            {
                var token = _loginWorkflow.Login(user);

                if (token.Equals(string.Empty))
                    return NoContent();

                return Ok(token);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult ValidateToken(string token, string username)
        {
            try
            {
                var user = _validateTokenWorkflow.Validate(token, username);

                if (user is null)
                    return NoContent();

                return Ok(user);
            }
            catch (Exception)
            {
                throw;
            }            
        }
    }
}