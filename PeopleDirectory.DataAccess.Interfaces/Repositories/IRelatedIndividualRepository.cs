using PeopleDirectory.DataContracts.Requests;

namespace PeopleDirectory.DataAccess.Interfaces.Repositories
{
    public interface IRelatedIndividualRepository
    {
        Task<bool> CheckRelationshipExistsAsync(int individualId, int relatedIndividualId, CancellationToken cancellationToken = default);
        
        Task CreateRelatedIndividualAsync(int id, CreateRelatedIndividualRequest request, CancellationToken cancellationToken);
        
        Task RemoveRelatedIndividualAsync(int relationId, CancellationToken cancellationToken);
    }
}
