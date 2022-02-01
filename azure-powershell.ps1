# Get the resource name
$result = (azure-naming -r app -p Titanic$(Get-Random) -c Web -e dev -f json | ConvertFrom-Json)

# Replace the following URL with a public GitHub repo URL
$gitrepo = "https://github.com/Azure-Samples/app-service-web-dotnet-get-started.git"

# Create a resource group
New-AzResourceGroup -Location westeurope -Name $result.ResourceGroup

# Create an App Service plan in Free tier
New-AzAppServicePlan -Location westeurope -Name $result.ResourceName -ResourceGroupName $result.ResourceGroup -Tier Free

# Create a web app
New-AzWebApp -Location westeurope -Name $result.ResourceName -ResourceGroupName $result.ResourceGroup -AppServicePlan $result.ResourceName

# Configure GitHub deployment from your GitHub repo and deploy once
$PropertiesObject = @{
    repoUrl = "$gitrepo";
    branch = "master";
    isManualIntegration = "true";
}
Set-AzResource -Properties $PropertiesObject -ResourceGroupName $result.ResourceGroup -ResourceType Microsoft.Web/sites/sourcecontrols -ResourceName "$($result.ResourceName)/web" -ApiVersion 2015-08-01 -Force

# Copy the result of the following command into a browser to see the web app
Write-Output "https://$($result.ResourceName).azurewebsites.net"
