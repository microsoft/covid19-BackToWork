# Step 2: Create the backend Azure SQL Database

For COVID-19 Back to Work solution, follow these steps to configure the Azure SQL database instance:

## Step 2.1 Create an Azure SQL database

1. Sign in to Azure [portal](https://portal.azure.com/). In the Search bar, type and select Azure SQL 
2. Click Add. On the Select SQL deployment option page, select the SQL databases tile, and Resource type = Single database. Click Create
3. In **Create SQL database** form under Basics, fill out the necessary details of subscription, resource group, database name 
4. Select pre-existing database Server. Or Create new and provide the necessary admin login details
5. Select No for "Want to use SQL elastic pool"
6. Under "Compute + storage", if you want to change the defaults, select Configure database.  After making necessary changes, select Apply	
7. Maintaining default values for other tabs, Click **Create**

** NOTE: For information on Azure SQL Single Database instance, it is highly recommended to go through these links and make necessary configuration changes: **
	
	- [Overview documentation](https://docs.microsoft.com/en-us/azure/azure-sql/database/single-database-overview)
	- [Security documentation](https://docs.microsoft.com/en-us/azure/azure-sql/database/security-overview)
	- [Firewall settings](https://docs.microsoft.com/en-us/azure/sql-database/sql-database-networkaccess-overview) 
	- [Add a private endpoint](https://docs.microsoft.com/en-us/azure/private-link/private-endpoint-overview)
	- [Server-level IP firewall rules](https://docs.microsoft.com/en-us/azure/azure-sql/database/firewall-create-server-level-portal-quickstart)

8. Click on the SQL server resource. On the left pane, go to Security -> Firewalls and virtual networks -> **Allow Azure services and resources to access this server** , set this to **Yes**

9. You will need a non-SA account to connect to Azure Function in the next Step [Step 3](https://github.com/microsoft/covid19-BackToWork/blob/master/WebchatChannel/3-DataConnection-AzureFunction.md). Create a user with least privileges to have the ***ability to execute stored procedures***  
	- Here is a template to follow to create a new user with ability to [Execute Stored Procedure](https://github.com/microsoft/covid19-BackToWork/blob/master/WebchatChannel/ExecuteStoredProc-SQLUserTemplate.md)
	- For more information on Database Users, please refer [here](https://docs.microsoft.com/en-us/sql/t-sql/statements/create-user-transact-sql?view=sql-server-ver15)
	
10. Replace the User Id and Password of the SQL Connection String with the new user's credentials created in Step 8 above

## Step 2.2 Download SSMS if not already setup

Download [SQL Server Management Studio (SSMS)](https://aka.ms/ssmsfullsetup). For more information, click [ here](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15)

## Step 2.3 Connect to your instance

1. Sign in to Azure [portal](https://portal.azure.com/)
2. Navigate to the SQL database or SQL managed instance you want to query
3. On the Overview page, copy the fully qualified server name. It's next to Server name for a single database, or the fully qualified server name next to Host for a managed instance. The fully qualified name looks like: servername.database.windows.net, except it has your actual server name
4. Open SSMS.
5. The Connect to Server dialog box appears. Enter the following information:
   
    | Setting        | Suggested value                 | Description                                                           |
    |----------------|---------------------------------|-----------------------------------------------------------------------|
    | Server type    | Database engine                 | Required value                                                       |
    | Server name    | The fully qualified server name | Something like: servername.database.windows.net                      |
    | Authentication | SQL Server Authentication       | Uses SQL Authentication                                              |
    | Login          | Server admin account user ID    | The user ID from the server admin account used to create the server  |
    | Password       | Server admin account password   | The password from the server admin account used to create the server |
6. Select Connect. The Object Explorer window opens
7. To view the database's objects, expand Databases and then expand your database node

## Step 2.4 Create tables, stored procedures and views

1. Set Context to use the appropriate database out of all available databases on this SQL Server
2. Execute the BackToWork-Tables&StoredProcedures.sql in [Scripts folder](https://github.com/microsoft/covid19-BackToWork/tree/master/Scripts) to create the required tables, stored procedures and views


