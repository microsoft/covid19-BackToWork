# Step 4: Send automated notications to registered users 

## Pre-requisites:
- All registered users should have provided a valid email address to get notified with a link of the bot 
- For info on sending SMS reminders, refer last section of this page. In this case, all registered users should provide a mobile number during registration

Azure functions are used to send automated notification either based on a 
1. HTTP trigger where administrator manually triggers the send notification function
2. Recurrence based trigger to send notification at a specific pre-determined time interval

In this GitHub repo, we provide an Azure function that will send email notification at a recurrence using SendGrid settings  

## Step 4.1: Setup SendGrid and add properties to Azure Functions Configuration
1. Go to Azure Portal, and search for SendGrid
2. Open SendGrid Accounts, and click Add. Fill out requested information and record the password in a secure place
3. Once the account is created, go back to SendGrid Accounts page, click on newly created account, and click manage. This will open up external management portal for SendGrid
4. Go to Settings on the left pane of the page, and click on "API Keys"
5. Enter a unique Key name and Create a Full Permission API key. Record the API key in a secure place as you will not be able to view it again
*For more details on SendGrid settings and functionality, refer [Azure functions binding with SendGrid](https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-sendgrid?tabs=csharp)

6. After you followed previous steps of this solution accurately and created all the Azure Functions, go to Azure portal -> BackToWorkFunctions -> Settings -> Configuration 
	- Create a +New Application Setting with Name = SendGrid_APIKEY, Value = API Key from Step 5 above
	- Create another application setting with Name = AssessmentBotLink, Value = link for the Back to work Assessment that you want your users to take
	- For local development on Visual Studio, you will have to add these settings in local.settings.json file, a sample template is shown in Step 3

## Step 4.2: Enable and modify Azure Function
If you followed previous steps of this solution accurately and created all the Azure Functions, go to Azure portal -> BackToWorkFunctions -> Functions. You will see 

| Name                | Trigger  | Status     |
|---------------------|----------|------------|
|TriggerNotification  | Timer    | Disabled   |

We need to modify 2 things for this function and this can be done in the cloned Visual Studio codebase.
Open your codebase and go to TriggerNotification.cs 
1) In Line 12, there is a <[Disable] > just above FunctionName("TriggerNotification"). Remove this line
2) Modify the ncrontab expression ("0 8 0 * * *") to suit a time based on your decisions. Find more details on ncrontab expressions [here](https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-timer?tabs=csharp#ncrontab-expressions)

Final code for an enabled trigger function running everyday at 8 am looks like:
```
[FunctionName("TriggerNotification")]
public static void RunAsync([TimerTrigger("0 8 0 * * *")]TimerInfo myTimer, ILogger log)
{
    //code to send email notifications         
}
```

**Note:** If you want to send SMS reminders with link of the bot:
Database lookup fetches Email Address and MobileNumber for each UserId. This will allow you to send notification to each user on their mobile phones. Setup requires 2 steps: 
1. Setup Twilio connection, details [here](https://docs.microsoft.com/en-us/azure/connectors/connectors-create-api-twilio)
2. Add a function similar to SendNotificationToAllRegisteredUsers(). For more details on using Twilio with Azure functions, please refer [Twilio bindings for Azure Functions](https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-twilio?tabs=csharp)


