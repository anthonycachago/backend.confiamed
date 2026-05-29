var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.BackEnd_Api>("backend-api");

builder.Build().Run();
