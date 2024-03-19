using System.Runtime.CompilerServices;

namespace BaseApi.Database
{
    public class MySQLDBSettings
    {
       public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["mysqlconnetion"];
            services.AddDBContext
        }

    }
}
