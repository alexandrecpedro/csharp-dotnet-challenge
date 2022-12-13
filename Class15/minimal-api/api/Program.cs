using MinimalApi;

namespace MinimalApi;

IHostBuilder CreateHostBuilder(string[] args){
  return Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
    });
}

CreateHostBuilder(args).Build().Run();

#region Mysql
#endregion

#region JWT
#endregion

#region Swagger
#endregion
