using fundooNotesCosmos.Context;
using fundooNotesCosmos.Entities;
using fundooNotesCosmos.Interface;
using fundooNotesCosmos.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace fundooNotesCosmos.Services
{
    public class UserServices:UserInterface
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;


        public UserServices(FundooContext fundooContext, IConfiguration configuration)
        {
                this.fundooContext = fundooContext;
            this.configuration = configuration;
            fundooContext.Database.EnsureCreated();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public UserEntity UserRegistrations(UserModel userModel)
        {
            try
            {
                UserEntity Entityuser = new UserEntity();
                Entityuser.FirstName = userModel.FirstName;
                Entityuser.LastName = userModel.LastName;
                Entityuser.EmailId = userModel.EmailId;
                Entityuser.Password = EncodePassword(userModel.Password);
                this.fundooContext.Add(Entityuser);
                int result = this.fundooContext.SaveChanges();
                if (result > 0)
                {
                    return Entityuser;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string EncodePassword(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string Decrypt(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] encryptedPass = Convert.FromBase64String(password);
                string decryptedPass = ASCIIEncoding.ASCII.GetString(encryptedPass);
                return decryptedPass;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>

        public string Login(LoginModel loginModel)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity = this.fundooContext.User.FirstOrDefault(x => x.EmailId == loginModel.EmailId);
                string pass = Decrypt(userEntity.Password);
                if (pass == loginModel.Password && userEntity != null)
                {
                    var token = this.GenerateJwtToken(userEntity.EmailId, userEntity.id);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmailId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public string GenerateJwtToken(string EmailId, string UserId)
        {
            try
            {
                var LoginTokenHandler = new JwtSecurityTokenHandler();
                var LoginTokenKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.configuration[("Jwt:Key")]));
                var LoginTokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("Email", EmailId.ToString()),
                        new Claim("UserId", UserId),
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(LoginTokenKey, SecurityAlgorithms.HmacSha256Signature),
                };
                var token = LoginTokenHandler.CreateToken(LoginTokenDescriptor);
                return LoginTokenHandler.WriteToken(token);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forgetPasswordModel"></param>
        /// <returns></returns>
        public ForgetPasswordModel UserForgetPassword(ForgetPasswordModel forgetPasswordModel)
        {
            try
            {
                var result = this.fundooContext.User.FirstOrDefault(x => x.EmailId == forgetPasswordModel.EmailId);

                if (result != null)
                {
                    forgetPasswordModel.Token = GenerateJwtToken(result.EmailId, result.id);
                    forgetPasswordModel.UserId = result.id;
                    MessageService.SendmessgeToQueue(forgetPasswordModel.EmailId, forgetPasswordModel.Token);
                    return forgetPasswordModel;
                }

                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="resetPasswordModel"></param>
        /// <returns></returns>
        public  ResetPasswordModel ResetPassword(string email, ResetPasswordModel resetPasswordModel)
        {
            try
            {
               var result = this.fundooContext.User.Where(x => x.EmailId == email).FirstOrDefault();
                result.Password = EncodePassword(resetPasswordModel.ConfirmPassword);
                this.fundooContext.SaveChanges();
                return resetPasswordModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     
       

    }
}
