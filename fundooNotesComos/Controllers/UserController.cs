using fundooNotesCosmos.Entities;
using fundooNotesCosmos.Interface;
using fundooNotesCosmos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace fundooNotesCosmos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserInterface userInterface;
        public UserController(UserInterface userInterface)
        {
           this.userInterface = userInterface;
        }
        [HttpPost("Register")]
        public IActionResult Register(UserModel registration)
        {

            try
            {
                var result = userInterface.UserRegistrations(registration);
                if (result != null)
                {
                    return Ok(new ResponseModel<UserEntity> { Status = true, Message = "UserRegistration Successful", Data = result });
                }
                else
                {

                    return BadRequest(new ResponseModel<UserEntity> { Status = false, Message = "UserRegistration Failed" });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginModel loginModel)
        {

            try
            {
                var result = userInterface.Login(loginModel);
                if (result != null)
                {

                    return Ok(new ResponseModel<string> { Status = true, Message = "Login Successful", Data = result });
                }
                else
                {

                    return BadRequest(new ResponseModel<string> { Status = false, Message = "Login Failed" });
                }

            }
            catch (Exception ex) { throw ex; }
        }

        [Authorize]
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                // string email = User.FindFirst("EmailId").Value;


                var email = User.FindFirst("Email").Value;
                //var email= HttpContext.Session.GetString("Email");

                var result = userInterface.ResetPassword(email, resetPasswordModel);
                if (result != null)
                {
                    return Ok(new ResponseModel<ResetPasswordModel> { Status = true, Message = "Password Reset Successful", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<ResetPasswordModel> { Status = false, Message = "Password Reset Failed", Data = result });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
