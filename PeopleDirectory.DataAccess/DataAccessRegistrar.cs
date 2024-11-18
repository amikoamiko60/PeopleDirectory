using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PeopleDirectory.DataAccess.Interfaces;
using PeopleDirectory.DataAccess.Interfaces.Repositories;
using PeopleDirectory.DataAccess.Repositories;

namespace PeopleDirectory.DataAccess
{
    public static class DataAccessRegistrar
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("PeopleDirectory")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IIndividualRepository, IndividualRepository>();
            services.AddScoped<IRelatedIndividualRepository, RelatedIndividualRepository>();
            services.AddScoped<ICityRepository, CityRepository>();

            return services;
        }
    }
}
