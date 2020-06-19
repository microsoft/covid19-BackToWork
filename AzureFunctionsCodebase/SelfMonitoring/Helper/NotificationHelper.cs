using System;
using SendGrid;
using SendGrid.Helpers.Mail;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using BackToWorkFunctions.Model;
using System.Collections.Generic;
using System.Linq;

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

        public static void SendSummaryEmailWithQRCode(string DestEmail, string SrcEmail, string AuthorName, string RecipientName, bool isClearToWork, SymptomsInfo symptomsInfo, string imageBase64Encoding, string SendGridAPIKey, string DateOfEntry)
        {
            try
            {
                var EmailClient = new SendGridClient(SendGridAPIKey);

                var EmailMessage = new SendGridMessage();
                EmailMessage.SetFrom(new EmailAddress(SrcEmail, AuthorName));
                EmailMessage.AddTo(DestEmail);
                EmailMessage.SetSubject("Return To Work Assessment Results");
                if (isClearToWork == true)
                {
                    var MessageContent = "<p>" + RecipientName + ", thank you for taking Screening Assessment.</p> <p><b>Summary</b>:</p> <p>Result: <font style=\"color: green\">You are cleared to work today.</font></p> <p>Date of Assessment: " + DateOfEntry + " UTC </p> <p>Please use the QR Code attached to show at your facility entrance.\n\n </p>";
                    EmailMessage.AddAttachment("qrcode.png", imageBase64Encoding);
                    EmailMessage.AddContent(MimeType.Html, MessageContent);
                }
                else
                {
                    List<String> lstOfSymptoms = new List<String>();

                    bool isSymptoms = false;
                    bool isExposed = false;

                    foreach (var property in symptomsInfo.GetType().GetProperties())
                    {
                        string propertyType = property.PropertyType.Name.ToString();
                        string propertyName = property.Name.ToString();
                        List<String> propertiesToExclude = new List<String> { "IsSymptomatic", "UserIsSymptomatic", "ClearToWorkToday"};
                        if (propertyType == "Boolean" && propertiesToExclude.Contains(propertyName) == false)
                        {
                            bool status = (bool)property.GetValue(symptomsInfo, null);
                            if (status == true)
                            {
                                if (propertyName == "UserIsExposed")
                                {
                                    isExposed = true;
                                }
                                else
                                {
                                    lstOfSymptoms.Add(propertyName.Replace("Symptom", ""));
                                    isSymptoms = true;
                                }
                            }
                        }
                    }
                    string symptomsStr = "";
                    if (isSymptoms == true)
                    {
                        symptomsStr = "You have the following symptoms today: ";
                        symptomsStr = symptomsStr + string.Join(", ", lstOfSymptoms);
                        symptomsStr = symptomsStr + ". ";
                    }
                    if (isExposed)
                    {
                        symptomsStr = symptomsStr + "You are currently in quarantine.";
                    }
                    var MessageContent = "<p>" + RecipientName + ", thank you for taking Screening Assessment.</p> <p><b>Summary</b>:</p> <p>Result: <font style=\"color: red\">You are NOT cleared to work today.</font></p> <p>Date of Assessment: " + DateOfEntry + " UTC </p> <p>Reaons: " + symptomsStr + "\n\n </p>";
                    EmailMessage.AddContent(MimeType.Html, MessageContent);
                }
                var EmailResponse = EmailClient.SendEmailAsync(EmailMessage);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static string GenerateQRCode(string GUIDforQR)
        {
            try
            {
                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                {
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(GUIDforQR, QRCodeGenerator.ECCLevel.Q);
                    using (QRCode qrCode = new QRCode(qrCodeData))
                    {
                        Bitmap qrCodeImage = qrCode.GetGraphic(20);
                        byte[] resultQR = null;
                        using (MemoryStream stream = new MemoryStream())
                        {
                            qrCodeImage.Save(stream, ImageFormat.Png);
                            resultQR = stream.ToArray();
                        }
                        string resultQRBase64 = Convert.ToBase64String(resultQR);
                        return resultQRBase64;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}