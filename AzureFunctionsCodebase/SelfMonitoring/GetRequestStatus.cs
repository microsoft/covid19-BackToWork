using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using BackToWorkFunctions.Model;
using System.Data.SqlClient;
using System.Text;
using BackToWorkFunctions.Helper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BackToWorkFunctions
{
    public static class GetRequestStatus
    {
        [FunctionName("GetRequestStatus")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetRequestStatus/{UserId}")] HttpRequest req, string UserId,
            ILogger log)
        {
            if (req == null || String.IsNullOrEmpty(UserId))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            try
            {
                RequestStatus requestStatus = await DbHelper.GetDataAsync<RequestStatus>(Constants.getRequestStatus, UserId).ConfigureAwait(false);
                if (requestStatus == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(requestStatus), Encoding.UTF8, "application/json")
                };
            }
            catch (ArgumentNullException argNullEx)
            {
                log.LogInformation(argNullEx.Message);
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            catch (Newtonsoft.Json.JsonSerializationException serializeEx)
            {
                log.LogInformation(serializeEx.Message);
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            catch (SqlException sqlEx)
            {
                log.LogInformation(sqlEx.Message);
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                log.LogInformation(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }
    }
}
