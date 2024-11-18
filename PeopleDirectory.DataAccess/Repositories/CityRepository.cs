using Microsoft.EntityFrameworkCore;
using PeopleDirectory.DataAccess.Interfaces.Repositories;

namespace PeopleDirectory.DataAccess.Repositories
{
    internal sealed class CityRepository(ApplicationDbContext db) : ICityRepository
    {
        public async Task<bool> CheckCityExistsAsync(int cityId, CancellationToken cancellationToken = default)
        => await db.Cities.Where(a => a.Id == cityId).AnyAsync(cancellationToken);
    }
}
