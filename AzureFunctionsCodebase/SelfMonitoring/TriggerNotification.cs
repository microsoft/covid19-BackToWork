using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using BackToWorkFunctions.Helper;
using BackToWorkFunctions.Model;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace BackToWorkFunctions
{
    public static class TriggerNotification
    {
        [Disable]
        [FunctionName("TriggerNotification")]
        public static void RunAsync([TimerTrigger("0 8 0 * * *")]TimerInfo myTimer, ILogger log)
        {
            string errorMessage;
            try
            {                
                if (myTimer == null)
                {
                    errorMessage = "Null timer argument";
                    throw new ArgumentNullException(errorMessage);
                }

                if (myTimer.IsPastDue)
                {
                    log.LogInformation("Timer is running late");
                }
                log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
                
                List<UserContactInfo> userContactInfoCollector = new List<UserContactInfo>();
                bool userContactsRetrieved = DbHelper.GetUserContactInfo(userContactInfoCollector);
                if (userContactsRetrieved)
                {
                    string sendgridApi = Environment.GetEnvironmentVariable("SendGrid_APIKEY", EnvironmentVariableTarget.Process);
                    if (String.IsNullOrEmpty(sendgridApi))
                    {
                        errorMessage = "SendGridAPI Key not found";
                        throw new ArgumentNullException(errorMessage);
                    }
                    string assessmentLink = Environment.GetEnvironmentVariable("AssessmentBotLink", EnvironmentVariableTarget.Process);
                    if (String.IsNullOrEmpty(assessmentLink))
                    {
                        errorMessage = "Assessment Link not found";
                        throw new ArgumentNullException(errorMessage);
                    }

                    foreach (UserContactInfo userContact in userContactInfoCollector)
                    {
                        bool emailSent = NotificationHelper.SendEmail(userContact.EmailAddress, "admin@contosohealthsystem.onmicrosoft.com", "Contoso Health System Admin",
                            assessmentLink, sendgridApi);
                        if (!emailSent)
                        {
                            log.LogInformation($"Error: Email could not be sent to UserId = {0} at email address = {1}", userContact.UserId, userContact.EmailAddress);
                        }
                    }
                }
                errorMessage = "Error in getting User details from database";
                throw new Exception(errorMessage);
            }
            catch (ArgumentNullException argNullEx)
            {
                log.LogInformation(argNullEx.Message);
                throw new ArgumentNullException(argNullEx.Message);
            }
            catch (Exception ex)
            {
                log.LogInformation(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
