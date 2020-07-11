using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using BackToWorkFunctions.Model;
using System.Data.SqlClient;
using BackToWorkFunctions.Helper;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackToWorkFunctions
{
    public static class PostSymptomsInfo
    {
        [FunctionName("PostSymptomsInfo")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestMessage req,
            ILogger log)
        {
            try
            {
                if (req == null)
                {
                    return new BadRequestObjectResult("Error: Request object missing");
                }
                SymptomsInfo symptomsInfo = await req.Content.ReadAsAsync<SymptomsInfo>().ConfigureAwait(false);
                if (checkEmptyOrNull(symptomsInfo))
                {
                    return new BadRequestObjectResult("Error: Incorrect payload");
                }
                
                bool dataRecorded = DbHelper.PostDataAsync(symptomsInfo, Constants.postSymptomsInfo);
                if (dataRecorded)
                {
                    string SrcEmail = Environment.GetEnvironmentVariable("SrcEmail", EnvironmentVariableTarget.Process);
                    string AuthorName = Environment.GetEnvironmentVariable("AuthorName", EnvironmentVariableTarget.Process);
                    string SendGridAPIKey = Environment.GetEnvironmentVariable("SendGridAPIKey", EnvironmentVariableTarget.Process);
                    if (!String.IsNullOrEmpty(SrcEmail) && !String.IsNullOrEmpty(AuthorName) && !String.IsNullOrEmpty(SendGridAPIKey))
                    {
                        UserContactInfo userContactInfo = DbHelper.GetSingleUserContactInfo(symptomsInfo.UserId.ToString());
                        string DestEmail = userContactInfo.EmailAddress.ToString();

                        string RecipientName = userContactInfo.FullName.ToString();
                        bool isClearToWork = symptomsInfo.ClearToWorkToday;
                        string recordGUID = "";
                        string imageBase64Encoding = "";
                        if (isClearToWork == true)
                        {
                            recordGUID = Guid.NewGuid().ToString();
                            symptomsInfo.GUID = recordGUID;
                            imageBase64Encoding = NotificationHelper.GenerateQRCode(recordGUID);
                        }

                        string DateOfEntry = symptomsInfo.DateOfEntry.ToString();

                        NotificationHelper.SendSummaryEmailWithQRCode(DestEmail, SrcEmail, AuthorName, RecipientName, isClearToWork, symptomsInfo, imageBase64Encoding, SendGridAPIKey, DateOfEntry);
                    }
                    return new OkObjectResult("Status: OK");
                }
                else
                {
                    return new BadRequestObjectResult("Error: Writing to database did not complete");
                }



            }
            catch (HttpRequestException httpEx)
            {
                log.LogInformation(httpEx.Message);
                return new BadRequestObjectResult("Error: Incorrect Request");
            }
            catch (ArgumentNullException argNullEx)
            {
                log.LogInformation(argNullEx.Message);
                return new BadRequestObjectResult("Error: Writing to database did not complete. One or more configuration parameters for SendGrid are missing.");
            }
            catch (Newtonsoft.Json.JsonSerializationException serializeEx)
            {
                log.LogInformation(serializeEx.Message);
                return new BadRequestObjectResult("Error: Incorrect payload");
            }
            catch (SqlException sqlEx)
            {
                log.LogInformation(sqlEx.Message);
                return new BadRequestObjectResult("Error: Writing to database did not complete");
            }
            catch (Exception ex)
            {
                log.LogInformation(ex.Message);
                return new BadRequestObjectResult("Error: Something went wrong, could not save your details");
            }
        }

        private static bool checkEmptyOrNull(SymptomsInfo symptomsInfo)
        {
            return symptomsInfo == null || String.IsNullOrEmpty(symptomsInfo.UserId) || String.IsNullOrEmpty(symptomsInfo.DateOfEntry)
                    || String.IsNullOrEmpty(symptomsInfo.UserIsExposed.ToString()) || String.IsNullOrEmpty(symptomsInfo.IsSymptomatic.ToString())
                    || String.IsNullOrEmpty(symptomsInfo.ClearToWorkToday.ToString());
        }
    }
}
