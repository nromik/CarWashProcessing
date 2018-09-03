using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace CarWashProcessing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }

    //Scaffold-DbContext "Server=localhost; Database=CarWashProcessing; Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir DataModels
    //
}
