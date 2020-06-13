1. Right click on SSMS, and select "Run as administrator"
2. Login with the admin user account
3. In the master database, execute the following query (Click on the master database, the highest level in the folder structure, and create a new query window)
   1. `USE master;`
   2. `CREATE LOGIN NEW_LOGIN_USERNAME WITH PASSWORD = 'NEW_LOGIN_PASSWORD'; GO` (This creates a new login credential for the master database)
   3. `CREATE USER NEW_LOGIN_USERNAME FOR LOGIN NEW_LOGIN_USERNAME; GO` (This creates a new user for the new login in the master database)
4. In individual database, execute the following query (You will have to click on the individual database, opening it up to the subfolder level, and then create a new query window)
   1. `USE INDIVIDUAL_DATABASE_NAME;`
   2. `CREATE ROLE db_execproc;` (This creates a new role that will be able to execute stored procedure)
   3. `CREATE USER NEW_LOGIN_USERNAME FROM LOGIN NEW_LOGIN_USERNAME;` (This creates a new user for the new login in the individual database)
   4. `EXEC sp_addrolemember 'db_execproc', 'NEW_LOGIN_USERNAME';` (This adds the new user to the db_execproc role, enabling the exeuction of stored procedure)
   5. `EXEC sp_addrolemember 'db_datawriter', 'NEW_LOGIN_USERNAME';` (This adds the new user to the db_datawriter role, enabling writing data)
   6. `EXEC sp_addrolemember 'db_datareader', 'NEW_LOGIN_USERNAME';` (This adds the new user to the db_reader role, enabling reading data)
   7. `GRANT EXECUTE ON SCHEMA::dbo TO db_execproc;` (This grants the db_execproc role the permission to execute stored procedure)