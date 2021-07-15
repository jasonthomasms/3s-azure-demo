---
languages:
- csharp
- aspx-csharp
page_type: sample
description: "This is a sample application that you can use to follow along with the Run a RESTful API with CORS in Azure App Service tutorial."
products:
- azure
- aspnet-core
- azure-app-service
---

# 3s Demo Info

This app creates a few different endpoints that you can hit and add code to. Take a look at `Controllers/AzureDemoController.cs` for how
to add endpoints/features. Note there's TodoList example code here, leaving for sample reference.

I'm building this using [this guide](https://docs.microsoft.com/en-us/azure/app-service/app-service-web-tutorial-rest-api)

## Building this application
Build in Visual Studio as usual, nothing special. Once up, you should be able to send get/post requests through postman at `localhost:5000/api/demo/<endpoint>?<params>`.

## Deploy on Azure

I've already created the app service and set up deployment credentials. You simply need to add the remote azure repo and push to it when changes are ready.

1. Download Azure cli if you don't have it.
2. Set deployment user. Get creds from Jason. `az webapp deployment user set --user-name <deployment-username> --password <deployment-password>`
2. Add Azure Remote. `git remote add azure https://<deployment-username>@azureintegrationwebservice.scm.azurewebsites.net/AzureIntegrationWebservice.git`
3. Push from local main to remote master whenever changes are ready to go in. `git push azure main:master
4. Go to `https://azureintegrationwebservice.azurewebsites.net/`. You'll see the todo landing page, API is there behind the scenes. You can hit `api/demo/<endpoint>` through postman.

## Next Steps:
1. Integrate calls to 3s (Aroon)
2. Setup on azure (Done)
4. Clean up ToDo References (Jason)
3. Integrate some other azure services (Aroon or Jason depending on time)

# ASP.NET Core API sample for Azure App Service

This is a sample application that you can use to follow along with the tutorial at 
[Run a RESTful API with CORS in Azure App Service](https://docs.microsoft.com/azure/app-service/app-service-web-tutorial-rest-api). 

## License

See [LICENSE](https://github.com/Azure-Samples/dotnet-core-api/blob/master/LICENSE.md).

## Contributing

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
  
