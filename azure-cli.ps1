# Get the resource name
$result = (azure-naming -r app -p Titanic$(Get-Random) -c Web -e dev -f json | ConvertFrom-Json)

# Replace the following URL with a public GitHub repo URL
$gitrepo = "https://github.com/Azure-Samples/app-service-web-dotnet-get-started.git"

# Create a resource group
az group create --location westeurope --name $result.ResourceGroup

# Create an App Service plan in FREE tier
az appservice plan create --name $result.ResourceName --resource-group $result.ResourceGroup --sku FREE

# Create a web app
az webapp create --name $result.ResourceName --resource-group $result.ResourceGroup --plan $result.ResourceName

# Deploy code from a public GitHub repository
az webapp deployment source config --name $result.ResourceName --resource-group $result.ResourceGroup --repo-url $gitrepo --branch master --manual-integration

# Copy the result of the following command into a browser to see the web app
echo "https://$($result.ResourceName).azurewebsites.net"
