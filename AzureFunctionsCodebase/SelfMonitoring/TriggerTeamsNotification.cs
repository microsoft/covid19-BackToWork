using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using BackToWorkFunctions.Helper;
using BackToWorkFunctions.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

namespace BackToWorkFunctions
{
    public static class TriggerTeamsNotification
    {
        [Disable]
        [FunctionName("TriggerTeamsNotification")]
        public static async Task Run([TimerTrigger("0 8 0 * * *")]TimerInfo myTimer, ILogger log)
        {            
            try
            {
                string errorMessage;
                if (myTimer == null)
                {
                    errorMessage = "Null timer argument";
                    throw new ArgumentNullException(errorMessage);
                }

                if (myTimer.IsPastDue)
                {
                    errorMessage = "Timer is running late!";
                    log.LogInformation(errorMessage);
                }
                log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
                
                List<TeamsAddressQuarantineInfo> teamsAddressQuarantineInfoCollector = new List<TeamsAddressQuarantineInfo>();
                bool userTeamsAddressReceived = DbHelper.GetTeamsAddress(teamsAddressQuarantineInfoCollector);
                if (userTeamsAddressReceived)
                {
                    Uri triggerUri = new System.Uri(Environment.GetEnvironmentVariable("Healthbot_Trigger_Call", EnvironmentVariableTarget.Process));
                    if (String.IsNullOrEmpty(triggerUri.ToString()))
                    {
                        errorMessage = "Healthbot Trigger Uri not found";
                        throw new ArgumentNullException(errorMessage);
                    }
                    string scenarioId = Environment.GetEnvironmentVariable("Healthbot_ScenarioId", EnvironmentVariableTarget.Process);
                    if (String.IsNullOrEmpty(scenarioId))
                    {
                        errorMessage = "Healthbot Scenario Id not found";
                        throw new ArgumentNullException(errorMessage);
                    }
                    string healthbotApiJwtSecret = Environment.GetEnvironmentVariable("Healthbot_API_JWT_SECRET", EnvironmentVariableTarget.Process);
                    if (String.IsNullOrEmpty(healthbotApiJwtSecret))
                    {
                        errorMessage = "Healthbot API_JWT_SECRET not found";
                        throw new ArgumentNullException(errorMessage);
                    }

                    string healthbotTenantName = Environment.GetEnvironmentVariable("Healthbot_Tenant_Name", EnvironmentVariableTarget.Process);
                    if (String.IsNullOrEmpty(healthbotTenantName))
                    {
                        errorMessage = "Healthbot Tenant Name not found";
                        throw new ArgumentNullException(errorMessage);
                    }

                    foreach (var element in teamsAddressQuarantineInfoCollector)
                    {
                        bool teamsPingSent = await ProgrammaticTrigger.PostTriggerToAllRegisteredTeamsClients(
                            element.UserId, element.TeamsAddress, triggerUri, scenarioId, healthbotApiJwtSecret, healthbotTenantName).ConfigureAwait(false);
                        if (!teamsPingSent)
                        {
                            log.LogInformation($"Error: Teams trigger could not be sent to UserId = {0}", element.UserId);
                        }
                    }
                }
                else
                {
                    errorMessage = "Error in getting User's MS Teams details";
                    throw new Exception(errorMessage);
                }
            }
            catch (ArgumentNullException argNullEx)
            {
                log.LogInformation(argNullEx.Message);
                throw new ArgumentNullException(argNullEx.Message);
            }
            catch (HttpRequestException httpReqEx)
            {
                log.LogInformation(httpReqEx.Message);
                throw new HttpRequestException(httpReqEx.ToString());
            }
            catch (Exception ex)
            {
                log.LogInformation(ex.Message);
                throw new Exception(ex.Message);
            }
        }        
    }
}
