using Microsoft.Extensions.DependencyInjection;
using PeopleDirectory.BusinessLogic.Interfaces.Services;
using PeopleDirectory.BusinessLogic.Services;

namespace PeopleDirectory.BusinessLogic
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
