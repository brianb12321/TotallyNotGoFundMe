using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using TotallyNotGuFundMe.Email;
using TotallyNotGuFundMe.Models;

namespace TotallyNotGuFundMe.Account
{
    public partial class ForgotPassword : Page
    {
        public IEmailService EmailService { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Forgot(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Validate the user's email address
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                DonationUser user = manager.FindByName(Email.Text);
                if (user == null || !manager.IsEmailConfirmed(user.Id))
                {
                    FailureText.Text = "The user either does not exist or is not confirmed.";
                    ErrorMessage.Visible = true;
                    return;
                }
                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send email with the code and the redirect to reset password page
                string code = manager.GeneratePasswordResetToken(user.Id);
                string callbackUrl = IdentityHelper.GetResetPasswordRedirectUrl(code, Request);
                string emailBody = "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>.";
                Task.Run(() => EmailService.SendEmailAsync(user.Email, "Reset Password", emailBody, emailBody).GetAwaiter().GetResult())
                    .GetAwaiter()
                    .GetResult();

                loginForm.Visible = false;
                DisplayEmail.Visible = true;
            }
        }
    }
}