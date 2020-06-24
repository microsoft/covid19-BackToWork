# Advanced Customization to logic of Back to Work solution in Healthcare Bot, Azure Functions and Azure SQL Database

To customize a COVID-19 Back to Work solution writing to Azure SQL database, follow these steps :
## Step 1: Change flow in healthcare bot scenario
1. To edit flow of scenario elements, refer [Authoring custom scenarios](https://docs.microsoft.com/en-us/healthbot/scenario-authoring/scenario-elements)
2. Action steps are used for writing relevant javascript code for processing data read from and to be written to Azure SQL db. For adding logic in javascript code, leverage Action element
3. For more connections to database (either write data to or get data from database), add Data connection calls at relevant flow points

## Step 2: If necessary, modify Azure SQL Database schema
If you are adding logic that cannot be supported by tables/columns available with this starter kit, you will have to modify the database schema to suit your requirements. Changes to schema in Azure SQL Database, follow same norms as that of SQL database on-premise.

### *Instructions on Adding/Modifying Stored Procedures*

We will be writing a stored procedure that selects the email address and mobile number for a given user in UserInfo table based on UserId and FullName

1. In Object Explorer, connect to an instance of Database Engine and then expand that instance.
2. Expand Databases, expand the your target database, and then expand Programmability.
3. Right-click Stored Procedures, and then click New Stored Procedure.
4. On the Query menu, click Specify Values for Template Parameters.
5. In the Specify Values for Template Parameters dialog box, enter the following values for the parameters shown.
   
    |         Parameter        |                 Value                |
    |:------------------------:|:------------------------------------:|
    | Author                   | Your name                            |
    | Create Date              | Today's date                         |
    | Description              | Return user's contact information    |
    | Procedure_name           | [DATABASE_NAME].spGetUserContactInfo |
    | @Param1                  | @UserId                              |
    | @Datatype_For_Param1     | nvarchar(50)                         |
    | Default_Value_For_Param1 | NULL                                 |
    | @Param2                  | @FullName                            |
    | @Datatype_For_Param2     | nvarchar(50)                         |
    | Default_Value_For_Param2 | NULL                                 |
    
6. Click OK.
7. In the Query Editor, replace the SELECT statement with the following statement:
   
    ```
    SELECT UserId, FullName, EmailAddress, MobileNumber
    FROM [dbo].UserInfo  
    WHERE UserId = @UserId AND FullName = @FullName;
    ```

8. To test the syntax, on the Query menu, click Parse. If an error message is returned, compare the statements with the information above and correct as needed.
9.  To create the procedure, from the Query menu, click Execute. The procedure is created as an object in the database.
10. To see the procedure listed in Object Explorer, right-click Stored Procedures and select Refresh.
11. To run the procedure, in Object Explorer, right-click the stored procedure name [DATABASE_NAME].spGetUserContactInfo and select Execute Stored Procedure.
12. In the Execute Procedure window, enter values for required parameters.

**Validate all user input. Do not concatenate user input before you validate it. Never execute a command constructed from unvalidated user input.**


## Step 3: Update Azure function accordingly
If you have updated the database schema in any manner, you will need to update the Azure functions. Follow these steps:
1. Download/clone code from [AzureFunctionsCodebase](https://github.com/microsoft/covid19-BackToWork/tree/master/AzureFunctionsCodebase) folder and open in local Visual Studio IDE
2. In BackToWorkFunctions -> Model -> {relevant table name}.cs, add the exact column name
3. Make changes in BackToWorkFunctions -> Helper -> DBHelper.cs class. Functions PostDataAsync (Insert into SQL) and GetDataAsync (Select from SQL) will need modification
4. Build and test the VS solution. Then right click on BackToWorkFunctions and Publish it to the same Azure function as before. For more details on publishing Azure function from local repo, refer [here](https://docs.microsoft.com/en-us/azure/azure-functions/functions-develop-vs#publish-to-azure)





