namespace PeopleDirectory.DataAccess.Interfaces.Repositories
{
    public interface ICityRepository
    {
      Task<bool> CheckCityExistsAsync(int cityId, CancellationToken cancellationToken = default);
    }
}
