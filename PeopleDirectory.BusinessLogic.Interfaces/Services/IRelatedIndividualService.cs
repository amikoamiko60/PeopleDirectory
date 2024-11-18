using PeopleDirectory.DataContracts.Requests;
using PeopleDirectory.DataContracts.Responses;

namespace PeopleDirectory.BusinessLogic.Interfaces.Services
{
    public interface IRelatedIndividualService
    {
        Task CreateRelatedIndividualAsync(int id, CreateRelatedIndividualRequest request, CancellationToken cancellationToken);
        Task<IEnumerable<RelationshipReportResponse>> GetRelationshipReportAsync(RelationshipReportRequest request, CancellationToken cancellationToken);
        Task RemoveRelatedIndividualAsync(int id, int relationId, CancellationToken cancellationToken);
    }
}
