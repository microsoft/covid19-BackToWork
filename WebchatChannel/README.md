# COVID-19 Back To Work solution with Microsoft Healthcare Bot and Azure platform 

**DISCLAIMER**
- The Healthcare bot Back To Work template comes with built-in support for FHIR persistence. Using Azure API for FHIR is our primary recommendation for healthcare organizations to provide data interoperability across different health systems. This section of the repo provides custom options for organizations who prefer to use Azure SQL Database as their backend data store for this solution
- This is an **ACCELERATOR KIT** to help you quickly build, test and deploy a custom MVP solution for your organization. Please bear in mind, **this solution captures PHI/PII** in terms of person's contact details and symptoms experienced. Please make sure to adhere to all compliances and regulatory policies applicable to your industry. All responsibility to configure the necessary Audit, Security and Network settings lies on individual organizations 
- This has not been stress-tested. All instructions, code and templates are subject to review, modification and extensive testing from the individual user 
- Microsoft’s platform provides the necessary capabilities by combining our Healthcare Bot service with the Azure platform as shown below:  

![](https://github.com/microsoft/covid19-BackToWork/blob/master/Screenshots/ReferenceArchitectureSQL.png)


## Method 1: Use ARM templates to deploy required Azure services
1. [Configure Healthcare Bot](https://github.com/microsoft/covid19-BackToWork/blob/master/WebchatChannel/1-Configure-HealthcareBot.md) host using web chat channel
2. Use [ARM template](https://github.com/microsoft/covid19-BackToWork/blob/master/WebchatChannel/ARM-Deployment.md) to deploy required Azure SQL Database and Azure Functions
3. Enable Azure function to [Send automated notifications for taking assessment](https://github.com/microsoft/covid19-BackToWork/blob/master/WebchatChannel/4-SendNotifications-AzureFunction.md) 
3. Create reports and dashboards with [Power BI for real-time visualization](https://github.com/microsoft/covid19-BackToWork/blob/master/WebchatChannel/5-Visualize-PowerBI.md)

## Method 2: Manual steps to help you understand all underlying details

1. [Configure Healthcare Bot](https://github.com/microsoft/covid19-BackToWork/blob/master/WebchatChannel/1-Configure-HealthcareBot.md) host using web chat channel 
2. [Create the backend Azure SQL Database](https://github.com/microsoft/covid19-BackToWork/blob/master/WebchatChannel/2-Createbackend-AzureSQLDatabase.md)
3. [Configure Data Connection calls to Azure function](https://github.com/microsoft/covid19-BackToWork/blob/master/WebchatChannel/3-DataConnection-AzureFunction.md) . This Azure function handles write to and read from backend database 
4. Enable Azure function to [Send automated notifications for taking assessment](https://github.com/microsoft/covid19-BackToWork/blob/master/WebchatChannel/4-SendNotifications-AzureFunction.md) 
5. Create reports and dashboards with [Power BI for real-time visualization](https://github.com/microsoft/covid19-BackToWork/blob/master/WebchatChannel/5-Visualize-PowerBI.md)

## Additional Resources
- Step-by-step instructions on getting started with Microsoft Healthcare Bot service are available in this [Tech Community Blog](https://techcommunity.microsoft.com/t5/healthcare-and-life-sciences/updated-on-5-24-2020-quick-start-setting-up-your-covid-19/ba-p/1230537)

More resources to be updated regularly.