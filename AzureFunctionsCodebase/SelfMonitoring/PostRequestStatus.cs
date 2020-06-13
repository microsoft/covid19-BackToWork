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
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace BackToWorkFunctions
{
    public static class PostRequestStatus
    {
        [FunctionName("PostRequestStatus")]
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

                RequestStatus requestStatus = await req.Content.ReadAsAsync<RequestStatus>().ConfigureAwait(false);
                if (requestStatus == null || String.IsNullOrEmpty(requestStatus.UserId) || String.IsNullOrEmpty(requestStatus.DateOfEntry))
                {
                    return new BadRequestObjectResult("Error: Incorrect payload");
                }

                bool dataRecorded = DbHelper.PostDataAsync(requestStatus, Constants.postRequestStatus);

                if (dataRecorded)
                {
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
                return new BadRequestObjectResult("Error: Writing to database did not complete");
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
    }
}
