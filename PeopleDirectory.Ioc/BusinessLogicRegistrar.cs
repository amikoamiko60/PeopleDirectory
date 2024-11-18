using Microsoft.Extensions.DependencyInjection;
using PeopleDirectory.BusinessLogic.Interfaces.Services;

namespace PeopleDirectory.Ioc
{
    public static class BusinessLogicRegistrar
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<IIndividualService, IndividualService>();
            services.AddScoped<IRelatedIndividualService, RelatedIndividualService>();

            return services;
        }
    }
}
