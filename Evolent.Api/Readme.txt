Evolent Web API Project
--------------------------------
This Restful Web API sample is created To provide interfaces to add ,edit and delete contacts and information using Repository pattern and UnitOfWork

Project Directory Contains Evolent.mdf database file and also script file which need to attach to 
sql server instance and need to change below connection string datasource in a web.config file.

    <add name="EvolentEntities" connectionString="metadata=res://*/EvolentModel.csdl|res://*/EvolentModel.ssdl|res://*/EvolentModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=AJAYSINH_JAGTAP\SQLSERVER2016;initial catalog=Evolent;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />

After change above connection string run Evolent.WebApi project sample.

First Need to Attach Evolent.mdf database file

This sample is created for Evolent web api Using Repositroy Pattern and UnitOfWork contains projects for
1] Evolent.DataModel
2] Evolent.Services
3] Evolent.WebApi
4] Evolent.WebApi.Tests
