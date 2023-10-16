using fundooNotesCosmos.Entities;
using fundooNotesCosmos.Models;
using fundooNotesCosmos.Services;

namespace fundooNotesCosmos.Interface
{
    public interface UserInterface
    {
        public UserEntity UserRegistrations(UserModel userModel);
        public string Login(LoginModel loginModel);
        public ForgetPasswordModel UserForgetPassword(ForgetPasswordModel forgetPasswordModel);
        public ResetPasswordModel ResetPassword(string email, ResetPasswordModel resetPasswordModel);

    }
}
