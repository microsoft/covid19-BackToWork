# COVID-19 Back to Work solution with Microsoft Healthcare Bot, Azure platform and Microsoft Teams

The COVID-19 Back to Work solution template is an **ACCELERATOR KIT** to help you quickly build, test and deploy a custom solution for your organization. Organizations across all industries, such as healthcare, education, retail, manufacturing, and financial services can use this highly customizable template. Microsoft’s platform provides the necessary capabilities by combining our Healthcare Bot service with the Azure platform and Microsoft Teams as shown below: 

![](https://github.com/microsoft/covid19-BackToWork/blob/master/Screenshots/referenceArchitecture.png)

A Back to Work solution overview with talk track is available in this [Back to Work Solution Overview Tech Community blog](https://techcommunity.microsoft.com/t5/healthcare-and-life-sciences/using-the-covid-19-back-to-work-template-in-microsoft-healthcare/ba-p/1425833)

## Step 1: Choose your preferred data store
### 1. Azure API for FHIR
- The Healthcare bot Back To Work template comes with built-in support for FHIR persistence. This is our primary recommendation for healthcare organizations to provide data interoperability from different health systems. 
- Step-by-step instruction guide on using the Healthcare Bot template with Azure API for FHIR is available in this [Back to Work Tech Community blog](https://techcommunity.microsoft.com/t5/healthcare-and-life-sciences/using-the-covid-19-back-to-work-template-in-microsoft-healthcare/ba-p/1425833)

### 2. Azure SQL Database
**Note:** Follow instructions below only if you want to persist data in Azure SQL Database.

For step-by-step instructions, follow the [Azure SQL persistence specific README.md](https://github.com/microsoft/covid19-BackToWork/blob/master/WebchatChannel/README.md). Below you can find high level steps and upcoming custom options:

## Step 2: Choose your preferred UI channel
Choose a UI channel to run this solution. Two options available using this GitHub are - Web Chat and Microsoft Teams (Instructions for using MS Teams coming shortly)

### Web Chat Channel for using healthcare bot in external or internal facing website
- Method 1: Use ARM templates to deploy required Azure services
- Method 2: Manual steps to help you understand all underlying details

Detailed steps for Method 1 and Method 2 are available in the [Webchat specific README.md](https://github.com/microsoft/covid19-BackToWork/blob/master/WebchatChannel/README.md)

List of Microsoft services required:
* Healthcare Bot
* Azure App Service
* Azure SQL Database
* Azure function apps
* Power BI Pro or Premium

## Additional Resources
- Step-by-step instructions on getting started with Microsoft Healthcare Bot service are available [here](https://techcommunity.microsoft.com/t5/healthcare-and-life-sciences/updated-on-5-24-2020-quick-start-setting-up-your-covid-19/ba-p/1230537)

More resources to be updated regularly. 

# Contributing

This project welcomes contributions and suggestions.  Most contributions require you to agree to a
Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit https://cla.opensource.microsoft.com.

When you submit a pull request, a CLA bot will automatically determine whether you need to provide
a CLA and decorate the PR appropriately (e.g., status check, comment). Simply follow the instructions
provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
