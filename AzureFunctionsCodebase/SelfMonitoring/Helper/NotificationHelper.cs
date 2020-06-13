using System;
using SendGrid;
using SendGrid.Helpers.Mail;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
namespace BackToWorkFunctions.Helper
{   
    public static class NotificationHelper
    {
        public static bool SendEmail(string DestEmail, string SrcEmail, string AuthorName, string AssessmentLink, string SendGridAPIKey)
        {
            if(String.IsNullOrEmpty(DestEmail) || String.IsNullOrEmpty(SrcEmail) || String.IsNullOrEmpty(AuthorName) || String.IsNullOrEmpty(AssessmentLink) || String.IsNullOrEmpty(SendGridAPIKey))
            { 
                return false;
            }

            var EmailClient = new SendGridClient(SendGridAPIKey);
            var EmailMessage = new SendGridMessage();

            EmailMessage.SetFrom(new EmailAddress(SrcEmail, AuthorName));
            EmailMessage.AddTo(DestEmail);
            EmailMessage.SetSubject("Return To Work: Today's Assessment");
            var MessageContent = "<p>Please take today's screening assessment before going to work, Thank you! \n\n <a href='" + AssessmentLink + "'>COVID-19 Return to Work Assessment</a> </p>";
            EmailMessage.AddContent(MimeType.Html, MessageContent);

            var emailResponse = EmailClient.SendEmailAsync(EmailMessage).ConfigureAwait(false);
            if(String.IsNullOrEmpty(emailResponse.ToString()))
            {
                return false;
            }
            return true;            
        }
    }
}