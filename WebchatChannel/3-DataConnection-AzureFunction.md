# Step 3: Use Azure functions to Read From and Write To Azure SQL database 

## Step 3.1: Manual steps to create Azure functions are provided below:

1. Download/Clone repo for codebase available at [AzureFunctionsCodebase](https://github.com/microsoft/covid19-BackToWork/tree/master/AzureFunctionsCodebase)
2. Open downloaded solution in Visual Studio and open Solution Explorer
3. To develop locally, please refer this [guide](https://docs.microsoft.com/en-us/azure/azure-functions/functions-develop-vs). You will need a ***local.settings.json*** file in the VS solution with a template like this:
```
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "DefaultEndpointsProtocol=https;AccountName={associated-storage-account-name};AccountKey={account-key};EndpointSuffix=core.windows.net",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "SqlConnectionString": "Server=tcp:{your-db-server-name};Initial Catalog={your-database-name};Persist Security Info=False;User ID={your-userID};Password={your-password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  }
}
```

4. Right click on the BackToWorkFunctions project and Rebuild solution (or Clean + Build solution)
5. Right click on BackToWork project -> Publish -> Publish -> Choose an Azure function to deploy this code and all functions as serverless Azure Function Apps. 
Please find detailed steps [here](https://docs.microsoft.com/en-us/azure/azure-functions/functions-develop-vs#publish-to-azure)
5. Sign in to Azure [portal](https://portal.azure.com/) , navigate to the Azure Function app created in previous step and go to its Settings -> Configuration -> Application settings
6. Click on +New application setting and add 
	- Name: SqlConnectionString 
	- Value: Connection String of Azure SQL database created in [Step 2](https://github.com/microsoft/covid19-BackToWork/blob/master/WebchatChannel/2-Createbackend-AzureSQLDatabase.md) in form similar to "Server=tcp:your-db-server-name;Initial Catalog=your-database-name;Persist Security Info=False;User ID=your-userID;Password=your-password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
	- **NOTE: Please make sure the UserID & Password in this Connection String has a user with least privileges required to execute the stored procedures **
7. Enable App Insights from Settings -> App Insights. Click on "Turn on Application Insights" and create a new resource or choose an existing App Insights resource
8. Enforce HTTPS requests only by navigating to Settings -> TLS/SSL settings. Set:
	- HTTPS Only: On
	- Minimum TLS Version: 1.2
9. Restrict CORS access: Don't use wildcards in your allowed origins list. Instead, list the specific domains from which you expect to get requests. For more information, see [CORS](https://docs.microsoft.com/en-us/azure/azure-functions/functions-how-to-use-azure-function-app-settings#cors)
10. On the left pane under Functions, click on Functions to get list of all deployed functions. **Get Function Url** of each function and paste it for use in Step 3.2 below. The Functions have a [Function level authorization scope](https://docs.microsoft.com/en-us/azure/azure-functions/security-concepts#function-access-keys) and will have a **code=** at the end of each Function Url

### Note:
- For securing Azure Function in your environment, take a thorough look at the following links:
	- [Securing Azure Functions](https://docs.microsoft.com/en-us/azure/azure-functions/security-concepts)
	- [Networking concepts](https://docs.microsoft.com/en-us/azure/azure-functions/security-baseline)

## Step 3.2: Integrating with Healthcare bot scenario

1. Navigate to the Healthcare Bot service admin portal of your instance created in Step 1 of this guide 
2. After importing the healthcare bot scenario available in [Scripts folder](https://github.com/microsoft/covid19-BackToWork/tree/master/Scripts) , go to Visual Designer mode of this scenario. You will notice Data Connection calls denoted by orange ovals. These elements serve in calling Rest API endpoints (in our case - Azure functions) to read from and write to Azure SQL database
3. For each data connection, the right name of the Azure function from Step 3.1.4 above has to be replaced with the dummy value provided for guidance. For example:
In Get Patient|Read SQL, Base URL = 'https://{azurefunctionsname}.azurewebsites.net/api/GetUserInfo/' + scenario.uniqueId + '?code={code}'
	- Replace **{azurefunctionsname}** with the right function name you provided in previous step 3.1
	- Replace **{code}** with the authentication code generated while creation and can be obtained in "Get Function Url" step


