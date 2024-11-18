using PeopleDirectory.DataContracts.Requests;
using PeopleDirectory.DataContracts.Responses;

namespace PeopleDirectory.DataAccess.Interfaces.Repositories
{
    public interface IIndividualRepository
    {
        Task CreateIndividualAsync(CreateIndividualRequest request,CancellationToken cancellationToken = default);
        
        Task DeleteIndividualAsync(int id, CancellationToken cancellationToken);

        Task EditIndividualAsync(int id, EditIndividualRequest request, CancellationToken cancellationToken = default);

        Task<GetIndividualResponse> GetIndividualByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<bool> CheckIndividualExistsAsync(int id, CancellationToken cancellationToken = default);
        
        Task<PaginatedIndividualsResponse> GetIndividualsAsync(SearchIndividualsRequest request, CancellationToken cancellationToken);
       
        Task<IEnumerable<RelationshipReportResponse>> GetRelationshipReportAsync(RelationshipReportRequest request, CancellationToken cancellationToken);

        Task SaveIndividualImageAsync(int id, string imagePath, CancellationToken cancellationToken = default);
    }
}
