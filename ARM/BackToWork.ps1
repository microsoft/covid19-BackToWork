# June 2020
# Microsoft Corporation
# This Runbook is used to automate the deployment of assets for the HealthBot solution (COVID-19 Return to Work template)
# https://github.com/nikitapitliya006/COVID19-ReturnToWork


$subscriptionId = Read-Host -Prompt "Enter your subscription ID (look for it in the portal)"
$projectName = Read-Host -Prompt "Enter a (new) Resource Group that is used for generating resources"
$location = Read-Host -Prompt "Enter an Azure location (e.g. westus, westus2, westcentralus, centralus, eastus, eastus2, etc)"
$adminUser = Read-Host -Prompt "Enter the SQL server administrator username"
$adminPassword = Read-Host -Prompt "Enter the SQl server administrator password" -AsSecureString
$databaseName = Read-Host -Prompt "Enter name for the Return to Work database"
Connect-AzAccount
Set-AzContext -subscriptionId $subscriptionId
$resourceGroupName = "${projectName}rg"

#Create the Resource Group
New-AzResourceGroup -Name $resourceGroupName -Location $location

#Create the SQL Database Server and the Database and import the tables from a backpac file hosted in blob
$outputs=New-AzResourceGroupDeployment -ResourceGroupName $resourceGroupName -TemplateFile "DeployBackToWorkAssets.json" -administratorLogin $adminUser -administratorLoginPassword $adminPassword -databaseName $databaseName
foreach($key in $outputs.Outputs.keys){
    if($key -eq "dbserverName") {
        $dbserverName = $outputs.Outputs[$key].value
    }
}
$connectionstring = "Server=tcp:${dbserverName}.database.windows.net:1433;Initial Catalog=${databaseName};Persist Security Info=False;User ID=${adminUser};Password=${adminPassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

#Create the Azure Function, then get the value of the uniquely generated function name and store it in $appName variable
$outputs=New-AzResourceGroupDeployment -ResourceGroupName $resourceGroupName -TemplateFile "DeployBackToWorkAzureFunctions.json"
foreach($key in $outputs.Outputs.keys){
    if($key -eq "NewAzureFunctionName") {
        $appName = $outputs.Outputs[$key].value
    }
}

#Use Web Zip deployment to publish the Azure Functions
az functionapp deployment source config-zip -g $resourceGroupName -n $appName --src "BackToWorkAzureFunctions.zip"

#Configure the Application Settings for the Azure Function
az functionapp config appsettings set -n $appName -g $resourceGroupName --settings SqlConnectionString=$connectionstring

echo "Resources created"
az resource list --query "[?resourceGroup=='$resourceGroupName'].{ name: name, flavor: kind, resourceType: type, region: location }" --output table
Read-Host -Prompt "Press [ENTER] to continue ..."