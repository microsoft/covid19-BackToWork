# Step 2: Create the backend Azure SQL Database and Azure functions using ARM Template

## Step 2.1: Run ARM template
1. Download or clone the repo to your local drive
2. Navigate to ARM folder, you will see 5 files
3. Sign in to Azure [portal](https://portal.azure.com/). Start Cloud Shell (right next to the Search bar)
4. Follow this document to [Persist files in cloudshell storage](https://docs.microsoft.com/en-us/azure/cloud-shell/persisting-shell-storage) and upload the 5 files from ARM-Template folder of the cloned GitHub repo
5. Run the command >./BackToWork.ps1 
6. The cloudshell will prompt you for user inputs to create database, please provide the necessary details:
	- Subscription ID
	- (new) Resource Group name
	- Azure location
	- SQL server administrator username
	- SQL server administrator password
	- Name for Back to work database
	- Sign in using the instructions provided in the prompt
![](https://github.com/microsoft/covid19-BackToWork/blob/master/Screenshots/ARMTemplate-Cloudshell.png)

7. Once you provide appropriate inputs, the script will kick off creation of necessary resources. In a few minutes (usually less than 10 minutes) you will see the completion prompt asking you to acknowledge by hitting Enter
![](https://github.com/microsoft/covid19-BackToWork/blob/master/Screenshots/ARMTemplate-CloudShellComplete.png)

8. Navigate to the Resource group created, you will find 7 resources created - 
![](https://github.com/microsoft/covid19-BackToWork/blob/master/Screenshots/ARMTemplate-Resources.png)

9. Basic configuration settings related to Azure SQL Server

	9.1: Click on the SQL server resource. On the left pane, go to Security -> Firewalls and virtual networks -> **Allow Azure services and resources to access this server** , set this to **Yes**

10. Basic configuration settings related to Azure SQL Database
	
	10.1: Click on the SQL database resource. On the left pane, go to Settings -> Connection strings and copy the ADO.NET (SQL authentication) string that looks like - 
**Server=tcp:{your-db-server-name};Initial Catalog={your-database-name};Persist Security Info=False;User ID={your-userID};Password={your-password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;**

	10.2: You will need a non-SA account to connect to Azure Function in the next Step [Step 3](https://github.com/microsoft/covid19-BackToWork/blob/master/WebchatChannel/3-DataConnection-AzureFunction.md). Create a user with least privileges to have the ***ability to execute stored procedures***  
		- Here is a template to follow to create a new user with ability to [Execute Stored Procedure](https://github.com/microsoft/covid19-BackToWork/blob/master/WebchatChannel/ExecuteStoredProc-SQLUserTemplate.md)
		- For more information on Database Users, please refer [here](https://docs.microsoft.com/en-us/sql/t-sql/statements/create-user-transact-sql?view=sql-server-ver15)

	10.3: Replace the UserId and Password of the SQL Connection String with this new user's credentials

11. Basic configuration settings related to Azure Functions
	
	11.1: Enable App Insights from Settings -> App Insights. Click on "Turn on Application Insights" and create a new resource or choose an existing App Insights resource
	
	11.2: Enforce HTTPS requests only by navigating to Settings -> TLS?SSL settings. Set:
			- HTTPS Only: On
			- Minimum TLS Version: 1.2
			
	11.3: Restrict CORS access: Don't use wildcards in your allowed origins list. Instead, list the specific domains from which you expect to get requests. For more information, see [CORS](https://docs.microsoft.com/en-us/azure/azure-functions/functions-how-to-use-azure-function-app-settings#cors)
	
	11.4: On the left pane under Functions, click on Functions to get list of all deployed functions. **Get Function Url** of each function and paste it for use in Step 2.2 below. The Functions have a [Function level authorization scope](https://docs.microsoft.com/en-us/azure/azure-functions/security-concepts#function-access-keys) and will have a **code=** at the end of each Function Url
	
**NOTE:** If Azure Functions do not work as expected, please follow the section at end of this page: **NOTE: Local development of Visual Studio code**
	

## Step 2.2: Integrate Azure services with Healthcare bot 
1. Navigate to the Healthcare Bot service admin portal of your instance created in Step 1 of this guide 
2. After importing the healthcare bot scenario available in [Scripts folder](https://github.com/microsoft/covid19-BackToWork/tree/master/Scripts) , go to Visual Designer mode of this scenario. You will notice Data Connection calls denoted by orange ovals. These elements serve in calling Rest API endpoints (in our case - Azure functions) to read from and write to Azure SQL database
3. For each data connection, the right name of the Azure function from Step 2.1 above has to be replaced with the dummy value provided for guidance. For example:
In Get Patient|Read SQL, Base URL = 'https://{azurefunctionsname}.azurewebsites.net/api/GetUserInfo/' + scenario.uniqueId + '?code={code}'
	- Replace **{azurefunctionsname}** with the right function name generated with a randomly selected name during ARM deployment in previous step 2.1
	- Replace **{code}** with the authentication code generated while creation and can be obtained in "Get Function Url" step

### Azure SQL Server administration and security guidelines:
- SQL database created has a default configuration of General Purpose: Serverless, Gen5, 4 vCores. Please make necessary changes to the configuration based on your requirements

- For information on Azure SQL Single Database instance, it is highly recommended to go through these links and make necessary configuration changes: 
	- [Overview documentation](https://docs.microsoft.com/en-us/azure/azure-sql/database/single-database-overview)
	- [Security documentation](https://docs.microsoft.com/en-us/azure/azure-sql/database/security-overview)
	- [Firewall settings](https://docs.microsoft.com/en-us/azure/sql-database/sql-database-networkaccess-overview) 
	- [Add a private endpoint](https://docs.microsoft.com/en-us/azure/private-link/private-endpoint-overview)
	- [Server-level IP firewall rules](https://docs.microsoft.com/en-us/azure/azure-sql/database/firewall-create-server-level-portal-quickstart)

- Additional resources for using SSMS:
	- If you do not have SSMS, Download [SQL Server Management Studio (SSMS)](https://aka.ms/ssmsfullsetup). *For more information, click [ here](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15)*
	- Learn how to connect to [Azure SQL using SSMS](https://docs.microsoft.com/en-us/azure/azure-sql/database/connect-query-ssms)
	
### Azure Function security and networking guidelines:
- For securing Azure Function in your environment, take a thorough look at the following links:
	- [Securing Azure Functions](https://docs.microsoft.com/en-us/azure/azure-functions/security-concepts)
	- [Networking concepts](https://docs.microsoft.com/en-us/azure/azure-functions/security-baseline)
	
#### NOTE: Local development of Visual Studio code
To develop locally for customization to Azure Function code, follow these steps:
1. Download/Clone repo for codebase available at [AzureFunctionsCodebase](https://github.com/microsoft/covid19-BackToWork/tree/master/AzureFunctionsCodebase)
2. Open downloaded solution **BackToWork.sln** in Visual Studio and open Solution Explorer
3. To develop locally, please refer this [guide](https://docs.microsoft.com/en-us/azure/azure-functions/functions-develop-vs). You will need a ***local.settings.json*** file in the VS solution with a template like this:
```
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "DefaultEndpointsProtocol=https;AccountName={associated-storage-account-name};AccountKey={account-key};EndpointSuffix=core.windows.net", /*This is the Connection String of an Azure Storage account you want to connect to this Azure Function*/
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "SqlConnectionString": "Server=tcp:{your-db-server-name};Initial Catalog={your-database-name};Persist Security Info=False;User ID={your-userID};Password={your-password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;" /*This is the Connection String of Azure SQL database created in "Step 2: Create backend Azure SQL Database" */
  }
}
```
4. Right click on the BackToWorkFunctions project and Rebuild solution (or Clean + Build solution)
5. Right click on BackToWork project -> Publish -> Publish -> Choose the same Azure function to deploy this code and all functions as serverless Azure Function Apps. Please find detailed steps [here](https://docs.microsoft.com/en-us/azure/azure-functions/functions-develop-vs#publish-to-azure)




