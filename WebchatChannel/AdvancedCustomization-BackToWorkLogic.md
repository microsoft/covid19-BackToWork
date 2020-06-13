# Advanced Customization to logic of Back to Work solution in Healthcare Bot, Azure Functions and Azure SQL Database

To customize a COVID-19 Back to Work solution writing to Azure SQL database, follow these steps :
## Step 1: Change flow in healthcare bot scenario
1. To edit flow of scenario elements, refer [Authoring custom scenarios](https://docs.microsoft.com/en-us/healthbot/scenario-authoring/scenario-elements)
2. Action steps are used for writing relevant javascript code for processing data read from and to be written to Azure SQL db. For adding logic in javascript code, leverage Action element
3. For more connections to database (either write data to or get data from database), add Data connection calls at relevant flow points

## Step 2: If necessary, modify Azure SQL Database schema
If you are adding logic that cannot be supported by tables/columns available with this starter kit, you will have to modify the database schema to suit your requirements. Changes to schema in Azure SQL Database, follow same norms as that of SQL database on-premise

## Step 3: Update Azure function accordingly
If you have updated the database schema in any manner, you will need to update the Azure functions. Follow these steps:
1. Download/clone code from [AzureFunctionsCodebase](https://github.com/microsoft/covid19-BackToWork/tree/master/AzureFunctionsCodebase) folder and open in local Visual Studio IDE
2. In BackToWorkFunctions -> Model -> {relevant table name}.cs, add the exact column name
3. Make changes in BackToWorkFunctions -> Helper -> DBHelper.cs class. Functions PostDataAsync (Insert into SQL) and GetDataAsync (Select from SQL) will need modification
4. Build and test the VS solution. Then right click on BackToWorkFunctions and Publish it to the same Azure function as before. For more details on publishing Azure function from local repo, refer [here](https://docs.microsoft.com/en-us/azure/azure-functions/functions-develop-vs#publish-to-azure)





