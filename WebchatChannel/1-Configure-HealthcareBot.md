# Step 1: Configure Healthcare Bot

To use COVID-19 Back to Work solution on a Website, follow these steps:

## Step 1.1: Configure a healthcare bot
1. Login to [https://portal.azure.com](https://portal.azure.com/) and search for **Microsoft Healthcare Bot** in the Azure Marketplace
2. Enter details choosing Plan: W1-Free and click “Subscribe”
3. In Azure portal, go to **Home | Software as a Service (SaaS)**, click on the healthcare bot instance. In Overview blade, click on "Manage Account". You will be redirected to Health Bot Service admin portal 
4. Download scenario "COVID19 Back to Work SQL.json" and spreadsheet "Localization - Custom strings for SQL Back to Work.xlsx" from the [Scripts folder](https://github.com/microsoft/covid19-BackToWork/tree/master/Scripts) . In healthcare bot portal -
	- using the left navigation pane, go to Scenarios -> Manage. Click Import and choose "COVID19 Back to Work SQL.json" without any naming changes (You can edit after import succeeds)
	- using the left navigation pane, go to Language -> Localization. Click Import and choose "Localization - Custom strings for SQL Back to Work.xlsx", Preview Changes and click Save

## Step 1.2: Configure Web Chat channel
1. In the left pane of Health bot admin portal, navigate to **Integration > Secrets**
2. Copy APP_SECRET and WEBCHAT_SECRET and keep it handy for Step 4 below
3. To deploy web chat to Azure, go to Github repository [link](https://github.com/Microsoft/HealthBotcontainersample) . Click “Deploy to Azure”
4. In Deploy to Azure config page, provide the desired configuration details and paste App Secret and Webchat Secret values from Step 2. Click Next -> Deploy
5. Follow the section **Creating the Web Chat Channel** in this [blog](https://techcommunity.microsoft.com/t5/healthcare-and-life-sciences/updated-on-4-2-2020-quick-start-setting-up-your-covid-19/ba-p/1230537) :
	- In public/index.js file, change triggered_scenario: { trigger:"covid19_backToWork_sql"} to begin the scenario 
	- For additional customization to website chat window


**Note:** 
- To implement other types of login methods, please refer [Additional-Login-Methods](https://github.com/microsoft/covid19-BackToWork/blob/master/WebchatChannel/Additional-Login-Methods.md)

#### Common UI changes to scenarios:
1. To edit the scenario logic without adding new symptoms or additional parameters, go to Scenarios -> Manage tab, click on the scenario and make necessary changes in Visual Designer or Code part. If you are planning to add more symptoms or a new workflow, changes need to be cascaded to Azure functions in Data Connection calls and the backend Azure SQL Database. For a detailed list of steps involved in end-to-end customization, refer [Advanced Customization to Logic](https://github.com/microsoft/covid19-BackToWork/blob/master/WebchatChannel/AdvancedCustomization-BackToWorkLogic.md)

2. Images of a green check mark and a red cross mark are available at [CheckMark](https://hbstenantasaeusprod.blob.core.windows.net/resources/contosohealthsystemteamsbot-g4ubxvv/CheckMark.png) and [Cross](https://hbstenantasaeusprod.blob.core.windows.net/resources/contosohealthsystemteamsbot-g4ubxvv/Cross.png) . Add these images (or other) in Resources -> Files tab in the healthcare bot admin portal. You can use them for denoting _Cleared to Work_ or _Not Cleared to Work_. Details on using Adaptive cards can be found [here](https://adaptivecards.io/) .One example of adding image in an adaptive card is shown below:
```
{
	"type": "Image",
	"url": conversation.resourcesUrl + "/CheckMark.png",
	"width": "100px",
	"height": "100px"				
}
```



