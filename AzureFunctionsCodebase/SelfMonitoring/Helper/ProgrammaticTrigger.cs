using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using BackToWorkFunctions.Model;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net;
using Microsoft.Azure.WebJobs.Logging;

namespace BackToWorkFunctions.Helper
{
    public static class ProgrammaticTrigger
    {        
        public static async Task<bool> PostTriggerToAllRegisteredTeamsClients(string UserId, string teamsAddress,
            System.Uri triggerUri, string scenarioId, string healthbotApiJwtSecret, string healthbotTenantName)
        {            
            try
            {
                if (!(String.IsNullOrEmpty(UserId)) && (String.IsNullOrEmpty(teamsAddress)))
                {
                    return false;
                }
                
                HttpClient client = new HttpClient();
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 ;
                client.BaseAddress = triggerUri;

                //Add an Authorization Bearer token (jwt token)
                string partial_token = GetJwtToken(healthbotApiJwtSecret, healthbotTenantName);
                if (String.IsNullOrEmpty(partial_token))
                {
                    client.Dispose();
                    return false;
                }
                string token = "Bearer " + partial_token;
                client.DefaultRequestHeaders.Add("Authorization", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                var payload = "{\"address\":" + teamsAddress + ",\"scenario\": \"/scenarios/" + scenarioId + "\"}";
                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(client.BaseAddress, content).ConfigureAwait(false);

                client.Dispose();
                content.Dispose();
                
                if (!response.IsSuccessStatusCode)
                {                   
                    response.Dispose();
                    return false;
                }

                response.Dispose();
                return true;
            }
            catch (Exception)
            {                   
                return false;
            }            
        }

        public static string GetJwtToken(string healthbotSecret, string healthbotName)
        {
            try
            {
                if (String.IsNullOrEmpty(healthbotSecret) || String.IsNullOrEmpty(healthbotName))
                {
                    return null;
                }
                
                TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
                int secondsSinceEpoch = (int)t.TotalSeconds;

                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(healthbotSecret));
                var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
                var header = new JwtHeader(signingCredentials);
                var payload = new JwtPayload
                {
                    { "tenantName", "" + healthbotName + ""},
                    { "iat", secondsSinceEpoch},
                };

                var secToken = new JwtSecurityToken(header, payload);
                var handler = new JwtSecurityTokenHandler();

                var tokenString = handler.WriteToken(secToken);
                return tokenString;
            }
            catch(ArgumentNullException argNullEx)
            {
                throw new ArgumentNullException(argNullEx.ToString());
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());                
            }
        }
    }    
}
