using Microsoft.Extensions.DependencyInjection;
using Students.Common.Contracts;
using Students.Common.Implementations;

namespace Students.Common
{
    public static class CommonExtensions
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            return services.AddScoped(typeof(IJsonManager<>), typeof(JsonManager<>));
        }        
    }
}